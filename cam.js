
var video = document.getElementById('video');
var _stream;
var canvas = document.getElementById('canvas');
var context = canvas.getContext('2d');
var video = document.getElementById('video');

var modal = document.getElementById('modalWindow');
var btn = document.getElementById("webCamBtn");
var span = document.getElementsByClassName("close")[0];

// When the user clicks on the button, open the modal 
btn.onclick = function() {
    modal.style.display = "block";
    initCam();
}

// When the user clicks on <span> (x), close the modal
var closeModal = function() {
    modal.style.display = "none";
    _stream.stop();
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
    if (event.target == modal) {
        modal.style.display = "none";
        _stream.stop();
    }
}

// Get access to the camera!
var initCam = function() {
    console.log("init cam");
    if(navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia({ video: true }).then(function(stream) {
            _stream = stream.getTracks()[0];
            video.src = window.URL.createObjectURL(stream);
            video.play();
        });
    }
}

var saveCam = function() {
    var image = canvas.toDataURL("image/png").replace("image/png", "image/octet-stream");
    console.log("Image saved, transfer to DB", image);
};



// Trigger photo take
document.getElementById("snap").addEventListener("click", function() {
	context.drawImage(video, 0, 0, 320, 240);
});
