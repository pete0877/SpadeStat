///////////////////////////////////////////////
// GenerateFooter() - returns content for the report footer (current date & time)
//
function GenerateFooter()
{
	var dtNow = new Date();
	return dtNow.toDateString();
}