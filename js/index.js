
// View Model for Questions
function mainViewModel() {
    var self = this;
    self.navigation = ko.observable();
    
    self.order = ko.observable("date");
    self.questionList = ko.observableArray();
    self.page = ko.observable(1);


    self.getQuestions = function() {
        console.log(self.order());
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

    self.question = ko.observable();
    self.order = ko.observable("date");
    
    self.getAnswers = 

}

$(document).ready(function(){
    var mainVM = new mainViewModel();
    ko.applyBindings(mainVM, $("#mainSection")[0]);

    var questionVM = new questionViewModel();
    ko.applyBindings(questionVM, $("#questionsSection")[0]);


    $.sammy(function() {
        this.get('#/', function(context) {
            mainVM.getQuestions();

            mainVM.navigation('main');
            questionVM.navigation('main');
        });
        
        this.get('#/question/:id', function(context) {
            questionVM.

            mainVM.navigation('question');
            questionVM.navigation('question');
        });
    }).run('#/');


});

