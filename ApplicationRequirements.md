# Requirement Specifications

### A user should be able to create, read, update, display and delete a request. 
	
* User shall consist of a username and a password they choose at account creation, these shall not be editable after creation. There shall also be fields for home address, email, and contact number that may be edited by user. There shall also be a binary field for driver status that is set to false and only changeable through administration and not readable by user.
	
* Upon entering the request screen there shall be five mandatory text fields, including the pickup and dropoff address, the pickup and dropoff names, and the delivery item.
	
* The create request screen shall also have 2 optional boxes for specific pickup and delivery instructions.

* The create request screen shall also have an optional "Upload File" button to attach screenshots of receipts from stores.
	
* User must be able to search a list of their requests and edit any open request fields, once a request is accepted by driver the fields shall be un-editable. If change is desired the user shall have option to contact driver directly.
	
* User must be able to cancel and remove any request that has not been accepted by a driver and can not be undone. 
	
### A driver has the option to accept any request in the display menu.
	 
* A driver shall extend from the user and shall have the binary field for driver status set to true. The driver shall have additional required information stored with their account, these will be a text field for the drivers licence, a seperate text field for the make, model, year, and licence plate number.
	
* A user must be able to request driver status from administration by submission of drivers licence, Car year, make, model, and licence plate.
	
* Drivers shall have a link form their user page that allows them to see a list of open requests and select a delivery they are able to perform. Once accepted the delivery shall be assigned to that driver. a driver may only commit to one delivery at a time.
	
* Once driver selects a delivery a generated email shall be sent to the requester with driver information (name, car information).
	
### A Request	

* A request must contain a text field for a pickup address and another text field for a drop off address. It must also have 2 binary states one for accepted for delivery which is set to true when a driver has accepted the delivery and shall block another driver from accepting the selected request. The other is for completion, this state will be the factor that determines if the request is displayed in the open request list for the drivers.
	
* A request shall be added to the end of the list and displayed to the drivers at the bottom.
	
* Any request submission must be validated for required field completion and proper input in the list of open requests.
	
* At completion of delivery a confirmation email will be sent to requesting user containing request number and date of completion.

## Event Driven Flow
![alt text](https://github.com/COSC481W-2019Winter/classproject-3-arabs-an-american/blob/master/src/common/images/EventDrivenBasedxml.png)

## Context Diagram
![alt text](https://github.com/COSC481W-2019Winter/classproject-3-arabs-an-american/blob/master/src/common/images/ContextDiagram.png)

## **Log-in Screen**
*  "Email" Text Box
  *  "Password" Text Box
  *  "Sign-in" Button
	-> Takes to "Home" Screen if correct
		-> Takes to the Driver variant of the "Home" Screen if account is registered to a driver
	-> Redirects back to "Log-in" Screen if incorrect
		-> Notifies user with red text
*  "Create New Account" Button
	-> Takes to "Create New Account" Screen
*  "About" Text

## Home Screen - General
*  "Create New Request" Button
	-> Takes to "Create New Request" Screen
*  "My Requests" Button
		-> Takes to "My Requests" Screen
	*  "Become a Driver" Button
		-> Takes to "Driver Registration" Screen
	
## Home Screen - Driver
*  "Create New Request" Button
	-> Takes to "Create New Request" Screen
*  "My Requests" Button
	-> Takes to "My Requests" Screen
*  "Requests Available for Pickup" Button
	-> Takes to "Requests Available for Pickup" Screen
*  "My Deliveries" Button
	-> Takes to "My Deliveries" Screen

## Create New Account Screen
*  "Name" Text Box
*  "Email" Text Box
*  "Phone Number" Text Box
*  "Home Address" Text Box
*  "New Password" Text Box
*  "Confirm Password" Text Box
*  "Create New Account" Button
	-> Takes to "Home" Screen if information above is valid
		-> All boxes completed
		-> Email not already attached to account
		-> "New Password" and "Confirm Password" text boxes contain same password
	Redirects back to "Create New Account" Screen if above criteria is not met
		-> Notifies user what went wrong in red text
	
## Driver Registration Screen
*  "Car Make" Text Box
* "Car Model" Text Box
*  "Car Year" Text Box
*  "Car License Plate" Text Box
*  "Submit" Button
	-> Submits information to be reviewed
	-> Displays hidden "Information Submitted" Text
	-> Redirects back to "Home" Screen after a couple seconds
	
## My Requests Screen
*  Shows the "Request History" of the user
*  Each request shows the status of each request (en route, delivered, awaiting driver, canceled, etc.)
*  Each request shows all of the information inputted when creating the request (deliver to, pickup from, etc.)
*  Each request shows the driver information if it has been accepted by a driver
*  Each request has an "Update Request" Button
	-> Takes to "Update Request" Screen if not already out for delivery
	-> Requests can NOT be updated if out for delivery
*  Each request has a "Cancel" Button
	-> Displays "Confirm Cancel" Pop-up
	-> Displays "Confirm Cancel" Button
		-> Changes status of request to "Canceled" if not already out for delivery
		-> Request is removed from the "Request Queue" if not already out for delivery
	-> Requests can NOT be canceled if out for delivery
	
## Create New Request Screen
*  "Deliver To" Text Box
*  "Delivery Address" Text Box
*  "Delivery Instructions" Text Box (Optional: For preferred delivery time and other special instructions)
*  "Pickup From" Text Box
*  "Pickup Address" Text Box
*  "Pickup Instructions" Text Box (Optional: For preferred pickup time and other special instructions)
*  "What is Being Delivered" Text Box
*  "Upload Files" Button (Optional: For uploading receipt/confirmation for online order)
	-> Opens "File Explorer" to choose file
*  "Checkout" Button
	-> Takes user to "Checkout" Screen
*  "Cancel" Button
	-> Displays "Confirm Cancel" Pop-up
	-> Displays "Confirm Cancel" Button
		-> Takes user to "Home" Screen
	
## Checkout Screen
*  Displays "Deliver To" Text
*  Displays "Delivery Address" Text
*  Displays "Delivery Instructions" Text
*  Displays "Pickup From" Text
*  Displays "Pickup Address" Text
*  Displays "Pickup Instructions" Text
*  Displays "What is Being Delivered" Text
*  Displays "Uploaded File" Text
*  "Confirm Order" Button
	-> Takes user to "Confirmation" Screen
	-> Adds request to the "Request Queue"
	-> Sets the request status to "Awaiting Driver"
	-> Adds request to the user's "Request History"
*  "Update Order" Button
	-> Takes user to "Update Request" Screen
	
## Confirmation Screen
*  Displays "Confirmation" Text
*  "Back to Home" Button
	-> Takes user back to the "Home" Screen
	
## Update Request Screen
*  "Deliver To" Text Box
	-> Has previous "Deliver To" information in it
*  "Delivery Address" Text Box
	-> Has previous "Delivery Address" information in it
*  "Delivery Instructions" Text Box (Optional: For preferred delivery time and other special instructions)
	-> Has previous "Delivery Instructions" information in it
*  "Pickup From" Text Box
	-> Has previous "Pickup From" information in it
*  "Pickup Address" Text Box
	-> Has previous "Pickup Address" information in it
*  "Pickup Instructions" Text Box (Optional: For preferred pickup time and other special instructions)
	-> Has previous "Pickup Instructions" information in it
*  "Upload Files" Button (Optional: For uploading receipt/confirmation for online order)
	-> Opens "File Explorer" to choose file
	-> Has previous file in it
*  "Update" Button
	-> Updates request information
	-> Takes user to "Confirmation" Screen
*  "Cancel" Button
	-> Retains request information
	-> Takes user to "Checkout" Screen if they came from the "Checkout" Screen
	-> Takes user to "My Requests" Screen if they came from the "My Requests" Screen
	
## Requests Available for Pickup Screen
*  Shows the "Request Queue" of all submitted requests in a certain mile radius
	-> Queue is ordered from oldest requests at the top to recently submitted at the bottom
*  Each request displays all of the information inputted when creating the request (deliver to, pickup from, etc.)
*  Each request has an "Accept Request" button
	-> Clicking adds the request to the top of the driver's "My Deliveries" List
	-> Clicking removes the request from the "Request Queue"
	-> Clicking updates the request status to "Awaiting Pickup"
	-> Clicking adds the driver's information to the request
	
## My Deliveries Screen
*  Shows the driver's "My Deliveries" List of all the deliveries that the driver has ever accepted
*  Each request shows the status of each request (en route, delivered, awaiting driver, canceled, etc.)
*  Each request shows all of the information inputted when creating the request (deliver to, pickup from, etc.)
*  Each request has an "Update Status" Dropdown that will change the status of the request
	-> Driver is to select "En Route" after picking up the delivery
	-> Driver is to select "Delivered" after completing the delivery
	-> Driver can select "Release Request" before picking up the delivery
		-> This will return the request to the top of the "Request Queue"
		-> This will remove the driver's information from the request
	
