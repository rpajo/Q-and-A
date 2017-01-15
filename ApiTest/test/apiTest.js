var supertest = require("supertest");
var should = require("should");
var api = supertest.agent("http://localhost:62713/api");

var user, questionId, commentId;

describe("User API Tests", function() {

	it("Create new test user", function(done) {
		var newUser = {"username" : "MochaTest", "email": "api@test.com", "password": "test"};
        api.post("/users")
        	.send(newUser)
        	.end(function (err, res) {
        		res.status.should.equal(200);
        		done();
        	});
    });

    it("Get another registered user", function(done) {
    	api.get("/users/1")
        	.end(function (err, res) {
        		res.status.should.equal(200);
        		done();
        	});
    });

    it("Login with new user", function(done) {
    	var login = {"email": "api@test.com", "password": "test"};
        api.put("/users/login")
        	.send(login)
        	.end(function (err, res) {
        		user = res.body;
        		res.status.should.equal(200);
        		done();
        	});
    });

    it("Edit new user settings", function(done){
		var newSettings = {"description": "this is a test description", "Location": "Web Api, Test"};

		api.put("/users/" + user.userId)
			.send(newSettings)
			.end(function(err, res) {
				res.status.should.equal(200);
				done();
			});
	});
});

describe("Question API Tests", function() {

	
	it("Post a new question", function(done) {
		var newQuestion = {"title": "Api Test Question", "description": "Test description", "userId": user.userId, "anonymous": 0, "author": "MochaTest"};
		api.post("/question")
			.send(newQuestion)
			.end(function(err, res) {
				res.status.should.equal(200);
				questionId = res.body;
				done();
			});
	});

	it("Fetch the new question", function(done) {
		api.get("/question/" + questionId)
			.end(function(err, res) {
				res.status.should.equal(200);
				done();
			});
	});

	it("Fetch recent question list", function(done) {
		api.get("/question/date/1")
			.end(function(err, res) {
				res.status.should.equal(200);
				res.body.length.should.be.above(0);
				done();
			});
	});

	it("Upvote new question", function(done) {
		api.put("/question/" + questionId)
			.send({"rating": 1})
			.end(function(err, res) {
				res.status.should.equal(200);
				res.text.should.equal("Question successfuly updated");
				done();
			});
	});

});

describe("Comment API tests", function() {

	it("Post new comment to question", function(done) {
		api.post("/comment/" + questionId)
			.send({"userId": user.userId, "paerntId": 0, "description": "This is a test comment", "author": "MochaTest"})
			.end(function(err, res) {
				res.status.should.equal(200);
				commentId = res.body;
				done();
			});
	});

	it("Get question comments - should be array of 1", function(done) {
		api.get("/comment/" + questionId)
			.end(function(err, res) {
				res.status.should.equal(200);
				res.body.length.should.equal(1);
				done();
			});
	});
});

describe("Delete test data from DB", function() {

	it("Delete new comment", function(done) {
		api.get("/comment/" + commentId)
			.end(function(err, res) {
				res.status.should.equal(200);
				done();
			});
	});

	it("Delete new question", function(done) {
		api.delete("/question/" + questionId)
			.end(function (err, res) {
				res.status.should.equal(200);
				done();
        	});
	});

	it("Delete new user", function(done) {
        api.delete("/users/" + user.userId)
			.end(function (err, res) {
				res.status.should.equal(200);
				done();
        	});
    });

});