
// get Questions from API
var getQuestions = function(order, page) {
    console.log("Fetching ", order, page);
    $.ajax({
        type: "get",
        url: "http://localhost:62713/api/question/" + order + "/" + page,
        dataType: "json",
        success: function (response) {
            console.log(response);
            return response;
        },
        error : function(err) {
            console.log("ERROR: ", err);
            return err;
        }
    });
};

getQuestions('rating', 1);

// View Model for Questions
function QuestionViewModel() {
    var self = this;

    self.order = ko.observable();
    self.questions = ko.observable();

    self.selectOrder = function(order) {
        self.questions = getQuestions(order, 1);
    };
};

$(document).ready(function(){
    ko.applyBindings(new QuestionViewModel());
});
