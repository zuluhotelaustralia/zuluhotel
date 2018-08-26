var cm=null;
document.onclick = new Function("show(null)")

function getPos(el,sProp) {
	var iPos = 0
	while (el!=null) {
		iPos+=el["offset" + sProp]
		el = el.offsetParent
	}
	return iPos
}

function show(el,m) {
	if (m) {
		m.style.display='';
		m.style.pixelLeft = getPos(el,"Left")
		m.style.pixelTop = getPos(el,"Top") + el.offsetHeight
		m.style.visibility="visible";
		m.style.filter="alpha(opacity=85)";
		m.style.zIndex="100"
	}
	if ((m!=cm) && (cm)) cm.style.display='none'
	cm=m
}

var timerID = null;
var timerID2 = null;
var timeOut = 1000;

function setMouseOver() {
	fTime = document.fTimeSelect.fTimes;
	timeOut = fTime[fTime.selectedIndex].value;
}
// clears timerID to prevent the removal of navsubmenu after mouseover
function onMenu() {
	clearTimeout(timerID);
}
// sets timer when leaving submenu to provide delayed close
function outMenu() {
	timerID = setTimeout("show(null)", timeOut);
}

function onMouseOver2(el,m) {
	// concatinates the varibles with the ids defined for each navmenu section and its navsubmenu
	// and builds a string like show(m1,ms1)
	timerID2 = setTimeout("show(".concat(el.id).concat(",").concat(m.id).concat(")"),750);
	
	// when mousing from navmenu1 to navmenu2 this prevents the closer of navmemu2's navsubmenu by clearing
	// the timerID set by mousing out of navmenu1
	onMenu();
}
function onMouseOut2() {
	// does not open the navsubmenu if hover legnth did not match the timer length set in timerID2
	clearTimeout(timerID2);
	// makes sure the navsubmenu closes after hovering on navmenu if we never mouse over it
	outMenu();
}