
var submitAnswer = function(qId) {
    console.log("Submiting answer to question id: " + qId);
    var answerText = document.getElementById("answer").value;
    console.log("Answer: " + answerText);
    document.getElementById("answer").value = "";
}

var submitQuestion = function() {
    console.log("Submiting question");
    var questionTitle = document.getElementById("questionTitle").value;
    var questionDesc = document.getElementById("questionDesc").value;
    var questionTags = document.getElementById("questionTags").value;
    var questionCategory = document.getElementById("questionCategory").value;
    console.log(questionTitle, questionDesc, questionTags, questionCategory);
}

var search = function() {
    console.log("searching...");
    var term = document.getElementById("searchTerm").value;
    var tags = document.getElementById("searchTags").value;
    var category = document.getElementById("searchCategory").value;
    console.log(term, tags, category);
    
}

var hideShow = function(div) {
    var form = document.getElementById(div);
    
    console.log(form.clientHeight);
    if (form.clientHeight) {
        form.style.height = '0';
    }
    else {
        console.log(measure.clientHeight);
        form.style.height = measure.clientHeight + 16 +'px';
    }
    
}

var caterogyList = ["Health & Fitness", "Technology", "Science", "Entertainment", "Sports", "Pets", "Bussiness"];

// search bar autocomplete
var dataList = document.getElementById('categories');
var searchInput = document.getElementById('categoryInput');

caterogyList.forEach(function(item) {
    // Create a new <option> element.
    var option = document.createElement('option');
    // Set the value using the item in the JSON array.
    option.value = item;
    // Add the <option> element to the <datalist>.
    dataList.appendChild(option);
});