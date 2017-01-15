
var apiUrl = "http://localhost:62713/";
//var apiUrl = "http://qoapi.azurewebsites.net/"

/** @constructor Main View Model 
 *
*/
function mainViewModel() {
    var self = this;
    self.navigation = ko.observable().publishOn("navigation");
    self.userLoggedIn = ko.observable().subscribeTo("logged");

    self.order = ko.observable("date");
    self.questionList = ko.observableArray();
    self.page = ko.observable(1);

    /** Submit question function
     * @param {string} title - Question title
     * @param {string} description - Question description
     * @param {int} anonymous - 0: not anonymous, 1: anonymous
     */
    self.submitQuestion = function(form) {
        var title = $("#questionTitle")[0].value;
        var description = $("#questionDesc")[0].value;
        var anonymous = ($('#anonymousQ').is(":checked")) ? 1 : 0;

        if ($.trim(title).length > 0 || $.trim(description).length > 0) {
            $.ajaxSetup({
                contentType : 'application/json'
            });
            $.ajax({
                type: "post",
                url: apiUrl + "api/question/",
                data: JSON.stringify({ "userId": self.userLoggedIn().userId, "author": self.userLoggedIn().username, "title": title, "description": description, "anonymous": anonymous }),
                success: function (response) {
                    //console.log(response);
                    self.getQuestions();
                    $("#questionTitle").val("");
                    $("#questionDesc").val("");
                    $("#submitQstatus").text("Question submited");

                },
                error: function(err) {
                    console.log(err);
                    $("#submitQstatus").text("Something went wrong - question not submited");
                }
            });
        }
        
        return true;

    };

    /** Get question list 
     * @param {string} order[global] - order questions by date, rating, no. answers
     * @param {string} page[global] - page of questions
     */
    self.getQuestions = function() {
        //console.log(self.order());
        $.ajax({
            type: "get",
            url: apiUrl + "api/question/" + self.order() + "/" + self.page(),
            success: function(response) {
                console.log(response);
                self.questionList(response);
            },
            error : function(err) {
                console.log("ERROR: ", err);
            }
        });

        return true;
    };

    /** Go to question details
     * @param {int} questionId - unique Id of a questions
     */
    self.goToQuestion = function(q) {
        location.hash = '/question/' + q.questionId;

        return true;
    };

    /** Submit question function
     * @param {int} page: -1: previous page, 1: next page
     */
    self.changePage = function(page) {
        var p = self.page();
        console.log(self.page(), self.questionList().length)
        if (self.page() + nav >= 1) {
            console.log(self.questionList().length)
            self.page(p + nav);
            self.getQuestions();
        }
        
    }

};

/** @namespace Question View Model
 */
function questionViewModel() {
    var self = this;
    self.navigation = ko.observable().subscribeTo("navigation");;

    self.userLoggedIn = ko.observable().subscribeTo("logged");
    self.qId = 0;
    self.question = ko.observable();
    self.answers = ko.observableArray();
    self.comments = ko.observableArray();
    self.order = ko.observable("-date");
    
    /** Get full qustion with answers
      * @param {int} qId[global] - unique question Id
     * @param {string} order[global] - order answers by date, -date, rating
     */
    self.getAnswers = function() {
        $.ajax({
            type: "get",
            url: apiUrl + "api/question/" + self.qId,
            dataType: "json",
            success: function (response) {
                response.comments = ko.observableArray([]);
                self.question(response);
                //console.log(self.question());

                $.ajax({
                    type: "get",
                    url: apiUrl + "api/answer/" + self.order() + "/" + self.qId,
                    dataType: "json",
                    success: function (response) {
                        response.forEach(function(element) {
                            element.comments = ko.observableArray([]);
                        });
                        self.answers(response);
                        console.log(self.answers());

                        self.getComments();
                    }
                });
            }
        });

        return true;
    }

    /** Get question/answer comments
     * @param {int} qId[global] - unique question Id
     */
    self.getComments = function() {
        $.ajax({
            type: "get",
            url: apiUrl + "api/comment/" + self.qId,
            dataType: "json",
            success: function (response) {
                self.comments(response);
                //console.log(self.comments());
                //console.log("Sorting comments");
                self.comments().forEach(function(comment) {
                    //console.log(comment);
                    if (comment.parentId == 0) {
                        //console.log("comment to question");
                        self.question().comments.push(comment);
                    }
                    else {
                        self.answers().forEach(function(answer) {
                            //console.log(answer);
                            if (answer.answerId == comment.parentId) {
                                //console.log("comment to answer with id: " + answer.answerId);
                                answer.comments.push(comment);
                            }
                        });
                        //console.log(self.answers());
                    }
                });
            }
        });

        return true;    
    };

    /** Submit Answer to question
     * @param {stirng} text[from html]
     * @param {int} qId[global] - unique question Id
     * @param {User} User[global] -> user.id, user.username
     */
    self.submitAnswer = function() {
        var text = $("#answer")[0].value;
        if ($.trim(text).length > 0) {
            $.ajaxSetup({
                contentType : 'application/json'
            });
            $.ajax({
                type: "post",
                url: apiUrl + "api/answer/" + self.qId,
                data: JSON.stringify({ "questionId": self.qId, "userId": self.userLoggedIn().userId, "author": self.userLoggedIn().username,"description": text}),
                success: function (response) {
                    //console.log(response);
                    self.getAnswers();
                    var text = $("#answer")[0].value = "";
                },
                error: function(err) {
                    console.log(err);
                }
            });
        }
        
        return true;
    };

    /** Post Comment to answer/question
     * @param {stirng} text[from html]
     * @param {int} qId[global] - unique question Id
     * @param {User} User[global] -> user.id, user.username
     * @param {int} answerId - unique answer Id
     */
    self.postComment = function(answerId) {
        var text = $("#comment" + answerId)[0].value;

        if ($.trim(text).length > 0) {
            $.ajaxSetup({
                contentType : 'application/json'
            });
            $.ajax({
                type: "post",
                url: apiUrl + "api/comment/" + self.qId,
                data: JSON.stringify({ "questionId": self.qId, "userId": self.userLoggedIn().userId, "parentId": answerId, "description": text, "author": self.userLoggedIn().username}),
                success: function (response) {
                    console.log(response);
                    //clear comments
                    self.question().comments([]);
                    self.answers().forEach(function(answer) {
                        answer.comments([]);
                    });

                    self.getComments();

                    var text = $("#comment" + answerId)[0].value = "";
                },
                error: function(err) {
                    console.log(err);
                }
            });
        }

        return true;
    };


    /** Rate Post +1/-1
     * @param {int} qId[global] - unique question Id
     * @param {int} answerId - unique answer Id
     * @param {int} rating - 1: rateUp, -1: rateDown
     */
    self.editPost = function(answerId, rating) {
        console.log("edit post", rating);
        var data;
        if (rating != 0 && rating != 'solved') data = JSON.stringify( {"rating": rating} );
        else if (rating == 'solved') data = JSON.stringify( {"solved": 1, "rating": 0});
        if (answerId == 0) {
            $.ajaxSetup({
                contentType : 'application/json'
            });
            $.ajax({
                type: "put",
                url: apiUrl + "api/question/" + self.qId,
                data: data,
                success: function (response) {
                    //self.getAnswers();
                    var temp = self.question();
                    temp.rating = temp.rating + rating;
                    self.question(temp);
                },
                error: function(err) {
                    console.log(err);
                }
            });
        }

        else {
            console.log(apiUrl + "api/answer/" + answerId);
            
            $.ajaxSetup({
                contentType : 'application/json'
            });
            $.ajax({
                type: "put",
                url: apiUrl + "api/answer/" + answerId,
                data: data,
                success: function (response) {
                    self.getAnswers();

                },
                error: function(err) {
                    console.log(err);
                }
            });      
        }
        
        return true;
    };

    /** Go to user page
     * @param {int} userId - unique user Id
     */
    self.goToUser = function(userId) {
        console.log(userId);
        location.hash = '/profile/' + userId;
    }

};

/**@namespace Widget View Model
 * 
 */
function widgetViewModel() {
    var self = this;

    self.userLoggedIn = ko.observable(0).publishOn("logged");       // 0 - user not logged in, else it stores the userId
    self.profileSection = ko.observable('profile').syncWith("profileSection");;

    /** Login user
     * @param {stirng} username[from html]
     * @param {string} password[from html]
     */
    self.login = function() {
        var username = $("#usernameLogin")[0].value;
        var password = $("#passwordLogin")[0].value;
        var status = $("#loginStatus");
        $("#btnLogin").attr("disable", true);

        $.ajaxSetup({
            contentType : 'application/json',
            processData : false
        });
        $.ajax({
            type: "put",
            url: apiUrl + "api/users/login",
            data: JSON.stringify({"email": username, "password": password}),
            success: function (user, response) {
                console.log(response);
                $("#btnLogin").attr("disable", false);
                
                self.userLoggedIn(user);
                /*if (id > 0) {
                    $.ajax({
                        type: "get",
                        url: apiUrl + "api/users/" + id,
                        dataType: "json",
                        success: function (user, response) {
                            console.log(user, response);
                            self.userLoggedIn(user);
                        }
                    });
                }*/
                //to do display error message                 
            },
            error : function(err){
                var responseErr = JSON.parse(err.responseText);
                status.text(responseErr.Message)
            }

        });

        return true;
    };

    /** Log out user
     * 
     */
    self.logOut = function() {
        self.userLoggedIn(0);

        return true;
    };

    /** Register new user
     * @param {stirng} username
     * @param {int} email
     * @param {User} password
     */
    self.register = function() {
        var regForm = $("#regForm :input");
        var status = $("#registerStatus");
        var btn = regForm[4];

        var username = regForm[0].value;
        var email = regForm[1].value;
        var pass = regForm[2].value;
        var pass2 = regForm[3].value;
        
        console.log(username, email, pass, pass2);

        if ($.trim(username).length == 0 || $.trim(email).length == 0 || $.trim(pass).length == 0) {
            status.text("Form not valid");
        }
        else if ( !(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)) ) {
            status.text("Email not valid");
        }
        else if (pass != pass2) {
            status.text("Passwords don't match");
        }
        else {
            $(btn).attr("disabled", true);
            $(btn).html("Working...");

            $.ajaxSetup({
                contentType : 'application/json',
                processData : false
            });
            $.ajax({
                type: "post",
                url: apiUrl + "api/users/",
                data: JSON.stringify({"username": username, "email": email, "password": pass, "description": ""}),
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    status.text("User Created");
                    $(btn).html("Done");
                },
                error: function(err) {
                    status.text("Username or email already in use");
                    console.log("Error ", err);
                    $(btn).attr("disabled", false);
                    $(btn).html("REGISTER");
                }
            });
        }
        

        return true;
    };

    /** Go to logged in profile
     * @param {stirng} userId - Id of logged user
     * @param {string} section - section of profile page:profile, recent, settings
     */
    self.goToProfile = function(userId, section) {
        location.hash = '/profile/' + userId;
        self.profileSection(section);        
    }
};

/**@namespace Profile View Model 
 * 
*/
function profileViewModel() {
    var self = this;

    self.navigation = ko.observable().subscribeTo("navigation");;
    self.userLoggedIn = ko.observable().syncWith("logged");

    self.profileSection = ko.observable('profile').syncWith("profileSection");;

    self.user = ko.observable();
    self.recentQuestions = ko.observableArray([]);
    //self.recentAnswers = ko.observableArray([]);

    /** Get data of a user
     * @param {int} userId - unique user Id
     */
    self.getUser = function(userId) {
        //console.log(self.userLoggedIn());
        if (self.userLoggedIn() != undefined && userId == self.userLoggedIn().userId) {
            self.user(self.userLoggedIn());
        }
        else {
            $.ajax({
                type: "get",
                url: apiUrl + "api/users/" + userId,
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    self.user(response);
                    self.recentQuestions([]);
                    $.ajax({
                        type: "get",
                        url: apiUrl + "api/users/" + userId + "/recent",
                        dataType: "json",
                        success: function (response) {
                            console.log(response);
                            if(response.length > 0) self.recentQuestions(response);
                            else self.recentQuestions([]);
                            //self.recentAnswers(response[1]);
                        }
                    });

                },
                error: function(err) {
                    console.log("Error: " + err);
                }
            });
        }

        return true;
    };

    self.hasSettings = function() {
        if (self.userLoggedIn() == undefined || self.user() == undefined) {
            return false;
        }

        else {
            return self.userLoggedIn().userId == self.user().userId;
        }
    };

    /** Update profile data
     * @param {stirng} location[from html] - user location
     * @param {string} description[from html] - user "about me" description
     * @param {int} userId[global] - userId of logged user
     */
    self.updateProfile = function() {
        var location = $("#editLocation")[0].value;
        var about = $("#editAbout")[0].value;
        $("#userUpdateStatus").text("----- saved");
        $.ajaxSetup({
            contentType : 'application/json'
        });
        $.ajax({
            type: "put",
            url: apiUrl + "api/users/" + self.user().userId,
            data: JSON.stringify({"location": location, "description": about}),
            success: function (response) {
                console.log(response);
                $.ajax({
                    type: "get",
                    url: apiUrl + "api/users/" + self.user().userId,
                    dataType: "json",
                    success: function (response) {
                        self.user(response);
                        self.userLoggedIn(response);
                        $("#userUpdateStatus").text("Changes saved");
                    },
                    error: function(err) {
                        $("#userUpdateStatus").text("Something went wrong");
                        console.log("Error", err);
                    }
                });
            },
            error: function(err) {
                $("#userUpdateStatus").text("Something went wrong");
                console.log("Error", err);
            }
        });

        return true;
    };
}

// Load view models on page loads   
$(document).ready(function(){
    var mainVM = new mainViewModel();
    ko.applyBindings(mainVM, $("#mainSection")[0]);

    var questionVM = new questionViewModel();
    ko.applyBindings(questionVM, $("#questionsSection")[0]);

    var widgetVM = new widgetViewModel();
    ko.applyBindings(widgetVM, $("#widgetSection")[0]);

    var profileVM = new profileViewModel();
    ko.applyBindings(profileVM, $("#profileSection")[0]);

    $.sammy(function() {
        this.get('#/', function(context) {
            mainVM.getQuestions();

            mainVM.navigation('main');
            //questionVM.navigation('main');
            $("[data-localize]").localize('lang', {language: newLang}); 
        });
        
        this.get('#/question/:id', function(context) {
            questionVM.qId = context.params.id;
            questionVM.getAnswers();
            mainVM.navigation('question');
            //questionVM.navigation('question');
            $("[data-localize]").localize('lang', {language: newLang}); 
        });

        this.get("#/profile/:userId", function(context) {
            mainVM.navigation('profile');
            profileVM.getUser(context.params.userId);
            $("[data-localize]").localize('lang', {language: newLang}); 
        });

        this._checkFormSubmission = function(form) {
            return (false);
        };
    }).run('#/');

    $("[data-localize]").localize("lang", {language:"en"});
    var newLang = "en"
    $('#changeLocale').change(function() { 
        newLang = $(this).val();
        console.log(newLang)
        $("[data-localize]").localize('lang', {language: newLang}); 
        //$('#greeting').val(greeting); 
        //$('#languages').val(newLang); 

    }).
    change();

});

var hideShow = function(div) {
    var form = document.getElementById(div);
        
        if (form.clientHeight) {
        form.style.height = '0';
    }
    else {
        var measure = document.getElementById(div+"Measure");
        form.style.height = measure.clientHeight + 16 +'px';
    }
    
}