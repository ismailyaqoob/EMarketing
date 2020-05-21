# EMarketing
The project contain the basic implementation of website based on Ecommerce buying and selling.

## Features
The websiote contain following features:
* Admin/user login.
* Admin can add category.
* Admin can delete category.
* User can view ads according to the category.
* User can post ads.
* User can search ads.
* User can delete ads that is posted by him or her.

## Technology
* The project is built on **Visual Studio 2019**
* Programming Language **C#**
* .Net Framework 4.7
* SQL Server Managemant Studio 18.0

## How to Compile?

First of all you need to download the project then you need **Visual Studio** to run the project. If you have already installed **Visual Studio** then go to the folder of **EMarketing** then open **EMarketing.sln** then press **F5** to execute the code. When you execute the code you might be having some databsae errors. To overcome this issue you have to copy the scripts from [Database Scripts.txt](https://github.com/ismailyaqoob/EMarketing/blob/master/Database%20Scripts.txt) file and compime them in **Mocrosoft SQL Server Management Studio** then you need to change the connection string shown in below image in **Web.config** file. 
**_Note: This Web.config file is present under the folder of EMarketing not in Views folder._**

<image src="ConStringImage.png"/>

**_Note: The project is built on .Net Framework 4.7 if you have older version then you can run the project by setting the version of .Net Framework according to you._**
