# List of components:
-------------------------------------------------
## Models:
   * MyDbContext.cs
   * AccountModel.cs
   * RequestModel.cs
   * AddressModel.cs
    
## Controllers:
   * AccountsController.cs
   * HomeController.cs
   * RequestController.cs
    
## View Models:
   * SignUpViewModel.cs
   * AccountsViewModel.cs
   * BecomeDriverViewModel.cs
   * CreateRequestViewModel.cs

## Views:
   #### 1. Shared:
   * Layout.cshtml
   #### 2. General Views:
   * Index.cshtml
   * Privacy.cshtml
   #### 3. Account Views:
   * Index.cshtml
   * Signup.cshtml
   * Login.cshtml
   * Register.cshtml
   * RegisterUser.cshtml
   * RegisterDriver.cshtml
   * BecomeDriver.cshtml
   #### 4.Request Views:
   * CreateRequestView.cshtml
   * EditRequestView.cshtml
   * DeleteRequestView.cshtml
   * ViewRequestView.cshtml
   * ViewAllRequestsView.cshtml
        
## Identity:
   * UserManager.cs
   * SigninManager.cs
   * RoleManager.cs
       
-------------------------------------------------
## MyDbContext:
This class represents our data access layer. it extends the IdentityDbContext class.
An instance of this class allow us to retreive and store accounts, requests, and addresses in the databse.

### Properties:
Name | Type | Description
---- | ---- | -----------
Accounts | DbSet **(AccountModel)** | Set of Accounts
Requests | DbSet **(RequestModel)** | Set of Requests
Addresses | DbSet **(AddressModel)** | Set of Addresses
Roles |  DbSet **(Roles)** | Set of Roles
    
### Functionality:
This class extends IdentityDbContext class, so it will have all the functionality of IdenityDbContext.
There are many functionalities in this class, like retrieve and store accounts, requests, addresses and roles from the  database. We also can filter objects in the DbSets by using extension methods of the Linq library.
    
### Connectors:
This class uses a connection string to connect to a database.
An instance of this class will be instantiated and passed to our controllers by the dependency injection engine that comes with .net Core.

-------------------------------------------------        
## AddressModel: 
  An instance of this class represents an address.

### Properties:
Name |Type |Description
---- |---- |-----------
StreetNumber|int|The user's street number
StreetName |String|The user's street name
City | String | The user's city
State| String | The user's state
ZipCode | int |The user's zip code

### Functionalities:
   Getters and setters for all properties
    
### Connectors:
   Both accounts and requests models will have a navigational properties for addresses.
   In the RequestModel we need 2 addresses for pick up and drop off. so we will have 2 navigational properties.
   In the AccountModel we need the address of the user, so we will have one navigational property.

-------------------------------------------------
## AccountModel: 
An object from class represents an account and encapsulates data about the account.
Note we will be using the same class for both users and driver. If the account is not a driver,
driver specific fields will be null and the Account role will be set to "User".
If the account chooses to become a driver, they will have to provide more information about their car. The role "Driver" will be added to their account.
Also note a driver can use the site as a user or as a driver since they have both roles.

### Properties:

Name |Type | Description
---- |---- | -----------
Username|String|Username for login
PasswordHash | String | The hashed password     
Email | String | User Email address
Address | Address | User's Address
PhoneNumber| String | The user's phone number
CarMake | String | The driver's car make
CarModel | String | The driver's car model
CarColor | String |The driver's car color
CarYear |int|The driver's car year
CarLicensePlate|String | The driver's car's license plate

### Functionalities:
Getters and setters for all properties.
    
### Connectors:
An instance of the MyDbContext class will retreive and store objects of the AccountModel type.

-------------------------------------------------
## RequestModel: 
An object from this class represents a request and encapsulates data about a specific request.

### Properties:

Name |Type|Description
---- |----|-----------
PickupAddress|Address|The pickup address of the request
DropoffAddress |Address|The dropoff address of the request
Item | String | The name of the item
PickupInstructions | String |Optional instructions for pickup
DropoffInstructions|String |Optional instructions for drop off

### Functionalities:
Getters and setters for all properties.

### Connectors:
An instance of the MyDbContext will retrieve and store objects of the RequestModel Type.

-------------------------------------------------

## AccountsController:
This class will contain all the action methods related to accounts, like signing up, logging in, and logging out.
    
### Properties:

Name|Type |Description
----|---- |-----------
_context | MyDbContext|an instance of the data access layer class MyDbContext
_signinManager | SignInManager | an instance of the SignInManager class
_roleManager | RoleManager | an instance of the RoleManager class

### Functionalities:

Name |Parameters|Return|Behavior
---- |----------|------|--------
Constructor|MyDbContext,SignInManager,RoleManager|Instance|Widens the scope of the parameters            
Signup (GET)|None|SignupView | Just returns the signup page.
Signup (POST)|SignUpViewModel|HomePage|Creates a new account and redirect to homepage
Login (GET) |None|LoginView|Just returns the LoginView
Login (POST)|LoginViewModel|HomePage |Checks if the user exist or not, and redirect to appropriate page                      
Logout (GET)|None |HomePage |Logs the user out
Driver(GET)|None|BecomeDriverView |Just returns the BecomeDriver form
Driver(POST)|BecomeDriverViewModel |HomePage |Populates the driver fields in the user account and adds the "Driver" role to the account.
                                                        

### Connectors:
When the user navigates to a specific route in the accounts controller like /accounts/signup, an instance of this class will be instantiated and the appropriate action method will be called depending on the route and the http verb associated with the request.
    
-------------------------------------------------

## RequestController:
This class will contain all the action methods related to requests. Creating them, deleting them, updating them, getting a list of them, etc..
    
Name|Parameters|Return|Behavior
----|----------|------|--------
Get(GET)|int id |ViewRequestView |Uses the data access layer to look up the specified                                                                           request using the provided id
Delete(GET)|int id |DeleteRequestView|shows the confirm delete page
Delete(POST)|RequestModel| HomePage | Deletes the request from the database
Edit (GET)|int id |EditRequestView |Shows the edit request page populating the appropriate fields to edit                     
Edit(POST)|RequestModel|HomePage|Updates the request in the database with the changes
All(GET)|None|ViewAllRequestsView|Looks up all the requests in the database

-------------------------------------------------
## SignUpViewModel:
An object from this class represents the view model of the sign up page.
We use this class to do client side validation. The form wont go through unless the form is valid.
    
### Properties:
Name|Type|Description|Attribute Decorators
----|----|-----------|--------------------
Username|String|Username for login|Required, DataType.Text
Password |String|The user's password|Required, DataType.Password
Email |String|The user's Email address|Required, DataType.Email
Address|Address|The user's Address| None
PhoneNumber |String| The user's phone number|DataType.Phone

### Functionalties:
Getters and setters for all properties
    
### Connectors:
This type will statically bind to the Signup.cshtml page making it a strongly typed view.

-------------------------------------------------
## LogInViewModel:
An object of this class represents the view model of the login page.
We use this class to do client side validation. The form wont go through unless the form is valid.
    
### Properties:

Name|Type|Description|Attribute Decorators
----|----|-----------|--------------------
Username|String |Username for login|Required, DataType.Text
Password|String|The user's password|Required, DataType.Password

### Functionalties:
Getters and setters for all properties
    
### Connectors:
This type will statically bind to the Login.cshtml page making it a strongly typed view.

-------------------------------------------------
## BecomeDriverViewModel:
An object from this class represents the view model of the become a driver page.
We use this class to do client side validation. The form wont go through unless the form is valid.
    
Name|Type|Description|Attribute Decorators
----|----|-----------|--------------------
CarMake|String|The driver's car make|Required
CarModel|String|The driver's car model|Required
CarColor|String|The driver's car color | Required
CarYear|int|The driver's car year|Required
CarLicensePlate|String|The driver's car's license plate|Required

### Functionalities:
Getters and setters for all properties
    
### Connectors:
This type will statically bind to the BecomeDriver.cshtml page making it a strongly typed view.
    
-------------------------------------------------    
### CreateRequestViewModel:
An instnace of this class represents the view model of the create request page.
We use this class to do client side validation. The form won go through unless its valid.
    
Name|Type|Description|Attribute Decorators
----|----|-----------|--------------------
PickupStreetName|String|The pickup street name|Required, DataType.Text
PickupStreetNumber|int|The pickup street number|Required, DataType.Number
PickupCity|String|The pickup city|Required, DataType.Text
PickupState|String|The pickup state|Required, DataType.Text
PickupZipCode|int|The pickup zip code|Required, DataType.Number
DropoffStreetName|String|The dropoff street name|Required, DataType.Text
DropoffStreetNumber|int|The dropoff street number|Required, DataType.Number
DropoffCity|String|The dropoff city|Required, DataType.Text
DropoffState|String|The dropoff state|Required, DataType.Text
DropoffZipCode|int|The dropoff zip code|Required, DataType.Number
Item|String |The name of the item |Required, DataType.Text
PickupInstructions|String |Optional instructions for pickup|   
DropoffInstructions|String|Optional instructions for drop off|
