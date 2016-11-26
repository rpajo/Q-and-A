var caterogyList = ["Health & Fitness", "Technology", "Science", "Entertainment", "Sports", "Pets", "Bussiness"];
var questionGet, answerGet;

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
        
        if (form.clientHeight) {
        form.style.height = '0';
    }
    else {
        var measure = document.getElementById(div+"Measure");
        form.style.height = measure.clientHeight + 16 +'px';
    }
    
}

var getQuestions = function(type) {
    if (questionGet != type) {
        console.log("fetch new data... ", type);
        questionGet = type;
    }
}

var getAnswers = function(type) {
    if (answerGet != type) {
        console.log("fetch new data... ", type);
        answerGet = type;
    }
}

var loadCategories = function() {
    // search bar autocomplete
    var dataList1 = document.getElementById('categoriesSearch');
    var dataList2 = document.getElementById('categoriesAsk');
    var searchInput = document.getElementById('categoryInput');

    caterogyList.forEach(function(item) {
        // Create a new <option> element.
        var option = document.createElement('option');
        // Set the value using the item in the JSON array.
        option.value = item;
        // Add the <option> element to the <datalist>.
        dataList1.appendChild(option);
        dataList2.appendChild(option);
    });
}

var activeTab = function() {
    var url = window.location.href;
    var tab = url.split('#')[1];
    console.log(tab);

    if (tab == "profile" || tab == undefined) {
        document.getElementById("tab1").checked = true;
    }
    else if (tab == "activity") {
        document.getElementById("tab2").checked = true;
    }
    else if (tab == "settings") {
        document.getElementById("tab3").checked = true;
    }
}