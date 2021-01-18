Feature: Zillow
	To get data realestate data from Zillow
	As a user
	I want to run a console application and get back a CSV file

Scenario: Running the console application returns a CSV with prices
	Given I want to get the following data from zillow
	| Price |
	| 10    |
	When I run the application
	Then I will get the price back in the CSV

Scenario: The output file should be created in the specified path
	Given I want the output path to be "output"
	When I run the application
	Then a file is created in the output path