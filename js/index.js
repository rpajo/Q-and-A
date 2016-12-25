
// View Model for Questions
function QuestionViewModel() {
    var self = this;
    self.order = ko.observable("date");
    self.questions = ko.observableArray();
    self.page = ko.observable(1);

    self.getQuestions = function() {
        console.log(self.order());
        $.ajax({
            type: "get",
            url: "http://localhost:62713/api/question/" + self.order() + "/" + self.page(),
            success: function(response) {
                console.log(response);
                self.questions(response);
            },
            error : function(err) {
                console.log("ERROR: ", err);
            }
        });
    };

    self.viewQuestion = function(q) {
        console.log(q);
    };

    self.getQuestions();
};

$(document).ready(function(){
    ko.applyBindings(new QuestionViewModel());
});
