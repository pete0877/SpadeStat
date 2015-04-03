///////////////////////////////////////////////
// GenerateMenu() - returns content of the menu.
//
function GenerateMenu()
{
	var result = '';
	result += '<a href="news.htm" class="menuLink">news & updates</a><p> \n';
	result += '<a href="screenshots.htm" class="menuLink">screen shots</a><p>\n';
	result += '<a href="features.htm" class="menuLink">features</a><p>\n';
	result += '<a href="download.htm" class="menuLink">download</a><p>\n';
	result += '<a href="purchase.htm" class="menuLink">purchase</a><p>\n';
	result += '<a href="support.htm" class="menuLink">support & FAQ</a><p>\n';
	result += '<a href="contact.htm" class="menuLink">contact us</a><p>\n';
	result += '<a href="index.htm" class="menuLink"><img src="image/home.gif" alt="SpadeStat.Com" border="0"><br>home</a><p>\n';
	return result;
}


///////////////////////////////////////////////
// onPageLoad() - Initializes page
//
function onPageLoad()
{
	document.getElementById('menu').innerHTML = GenerateMenu();
}


///////////////////////////////////////////////
// PopUpScreenShot() - Pops up screen shot preview page
//
function PopUpScreenShot(path)
{
	window.open(path, 'Screenshot', 'scrollbars,resizable,width=800,height=600');	
}

