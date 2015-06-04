#pragma strict

var pianoClip:AudioClip;
var isPlaying = false;

function OnTriggerEnter(o:Collider){
	if(isPlaying == false){
		GetComponent.<AudioSource>().Play();
	}
	isPlaying = true;
}

function OnTriggerExit(o:Collider){
	if(isPlaying == true){
		GetComponent.<AudioSource>().Pause();
	}
	isPlaying = false;
}
