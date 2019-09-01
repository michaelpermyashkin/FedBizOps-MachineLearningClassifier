var fs = require("fs");
var parser = require("./index");

/*
 * This class will read the Sudo-XML files downloaded from ftp://ftp.fbo.gov/ and output a JSON file that can then be used to train the classifier model
 */

fs.appendFileSync(/*Specify location to save output*/, JSON.stringify(parser.parse(fs.readFileSync(/*Where you woud like to read the data from*/, 'UTF-8'))));

