# FedBizOps-MachineLearningClassifier
A project that implements multi-class classification using ML.NET technology.

## Purpose
I was tasked with creating a model using ML.NET that could train on data provided by https://www.fbo.gov. I soon discovered an ftp endpoint which contained daily and weekly dumps of listings for the given day or week, respectively. That site is located here: ftp://ftp.fbo.gov/. 

This project is part of the final solution - at the time I wrote this, true RFP data was not yet available for the model to consume. As an update, I completed an FBOSpider(available on my github also) which has successfully gotten copious amounts of data which can now be used to train the model on RFP document classification. I am unsure yet how I can feed the various file formats such as PDF and DOCX into the model and have it read the documents. So I suppose the next step is to modify this project to include logic which solves this problem. 

I found that a straight forward solution was tough to find, so this will hopefully help you with a similar task.

## Project Contents
In this repository you will find 2 things:
* FBO.gov daily dump parser 
* ML.NET project

#### FBO Parser
This is a parser I used from [this](https://github.com/presidential-innovation-fellows/fbo-parser) project. I made several changes to the Node.js project to reformat the JSON output file. This parser is specifically for the daily FBO dumps which are composed of psuedo-xml. I found this to be the prefered choice of data because the weekly digests are less consistant in structure.

#### ML.NET Project
The ML.NET project is a program that uses the JSON files provided from the modified FBO parser to train a model to perform multi-class classification. Among other things, the program can also write text files containing specified properties of the JSON. For example, it is able to compile a list of all unique Agencies, Offices and NAICS codes found in the training data set.

## Getting started

#### What you will need
* **Visual Studio 2017** or better with **.NET Core cross-platform development** and **Node.js Developement** workloads installed
* **FBO daily digest files** which will serve as the data set for training
* **Clone the project** 

#### Getting the data and converting file to JSON
* Download the desired daily digest files from ftp://ftp.fbo.gov/
* Open **example.js** in FBO-Parser and specify locations of the downloaded files & desired destination
* Run example.js

#### Training the model with JSON
* Open JSON-RFP-Classifier Solution in Visual Studio
* Place JSON files inside **Data Directory**
* Uncomment **TrainModel() method in Program.cs**
* Run Program.cs

If you would like to have the program compile vocabulary from the data set (Agency names, Offices, etc.), ensure the **TermList method** is also uncommented withing Program.cs. Inside the **Term Compiler Class** you can specify which properties within the JSON file you would like to have compiled.

Note: I suggest not doing both tasks in the same pass. Training the model is a heavy process, and writing vocab to files can take some time as well. I would run the jobs seperately if you are processing a large amount of data.

## Built with
* [ML.NET](https://dotnet.microsoft.com/learn/ml-dotnet/get-started-tutorial/install) 

## Author
* Michael Permyashkin

## Acknowledgements
* [fbo.gov](https://www.fbo.gov) - Provided data via ftp
* [presidential-innovation-fellows](https://github.com/presidential-innovation-fellows/fbo-parser) - FBO daily parser
* [Multi-class Classifier in ML.NET](https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/github-issue-classification) - An amazing reference
