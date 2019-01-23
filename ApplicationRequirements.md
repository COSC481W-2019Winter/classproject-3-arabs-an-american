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
		
	
