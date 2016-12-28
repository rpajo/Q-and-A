
// View Model for Questions
function mainViewModel() {
    var self = this;
    self.navigation = ko.observable();
    
    self.order = ko.observable("date");
    self.questionList = ko.observableArray();
    self.page = ko.observable(1);


    self.getQuestions = function() {
        //console.log(self.order());
        $.ajax({
            type: "get",
            url: "http://localhost:62713/api/question/" + self.order() + "/" + self.page(),
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

    self.bla = function(q) {
        location.hash = '/question/' + q.questionId;

        return true;
    }

};


function questionViewModel() {
    var self = this;
    self.navigation = ko.observable();

    self.qId = 0;
    self.question = ko.observable();
    self.answers = ko.observableArray();
    self.comments = ko.observableArray();
    self.order = ko.observable("rating");
    
    self.getAnswers = function() {
        //console.log(self.order());  
        $.ajax({
            type: "get",
            url: "http://localhost:62713/api/question/" + self.qId,
            dataType: "json",
            success: function (response) {
                response.comments = ko.observableArray([]);
                self.question(response);
                console.log(self.question());

                $.ajax({
                    type: "get",
                    url: "http://localhost:62713/api/answer/" + self.order() + "/" + self.qId,
                    dataType: "json",
                    success: function (response) {
                        response.forEach(function(element) {
                            console.log("ADD ARRAY")
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

    self.getComments = function() {
        $.ajax({
            type: "get",
            url: "http://localhost:62713/api/comment/" + self.qId,
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
                        console.log(self.answers());
                    }
                });
            }
        });

        return true;    
    }

    self.submitAnswer = function() {
        var text = $("#answer")[0].value;
        var body = { };
    }

}

var widgetViewModel = function() {
    var self = this;

    self.userLoggedIn = ko.observable(0);       // 0 - user not logged in, else it stores the userId

    self.login = function() {
        var username = $("#usernameLogin")[0].value;
        var password = $("#passwordLogin")[0].value;

        $("#btnLogin").attr("disable", true);
        $("#btnLogin").text("Logging in");
        $.ajaxSetup({
            contentType : 'application/json',
            processData : false
        });
        $.ajax({
            type: "put",
            url: "http://localhost:62713/api/users/login",
            data: JSON.stringify({"email": username, "password": password}),
            success: function (xhr, response) {
                //console.log(xhr, response);
                $("#btnLogin").attr("disable", false);
                $("#btnLogin").text("LOG IN");;
                
                self.userLoggedIn(xhr);
                console.log(self.userLoggedIn());
            },
            error : function(err){
                console.log(err);
            }
        });

        return true;
    }

    self.logOut = function() {
        self.userLoggedIn(0);

        return true;
    }

}

$(document).ready(function(){
    var mainVM = new mainViewModel();
    ko.applyBindings(mainVM, $("#mainSection")[0]);

    var questionVM = new questionViewModel();
    ko.applyBindings(questionVM, $("#questionsSection")[0]);

    var widgetVM = new widgetViewModel();
    ko.applyBindings(widgetVM, $("#widgetSection")[0]);

    $.sammy(function() {
        this.get('#/', function(context) {
            mainVM.getQuestions();

            mainVM.navigation('main');
            questionVM.navigation('main');
        });
        
        this.get('#/question/:id', function(context) {
            questionVM.qId = context.params.id;
            questionVM.getAnswers();
            mainVM.navigation('question');
            questionVM.navigation('question');

        });
    }).run('#/');


});

