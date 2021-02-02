# PhoneBook

PhoneBook is a .Net Core Project for simple book for your contacts with several contact types (phone, mail and location).
You can save multiple contact types for your records.

## Usage

* Get all persons

You can take all the persons in the book

 API EndPoint :``` /Persons```
 

 * Get Person by ID

 You can select one person from the list
 
 API EndPoint :``` /Persons/GetPerson?id=[personID]```

 * Get Persons Form Locations

 You can select all the persons in a location
 
 API EndPoint :``` /Persons/GetPersonByLocation?ltd=[latitute]&lgt=[longtitute]```
 
  * Get Locations with additional infos

 You can take all the locations with locations point,person count in that location, phone count in that location an all persons in that locations
 
 API EndPoint :``` /Persons/GetLocaitonsWithPersonCount```
 
   * Insert a person

 You can add a person to book
 
 API EndPoint :``` /Persons/Insert```
 
   * Edit a person
   
 You can edit a person's info in the book
 
 API EndPoint :``` /Persons/Update?id=[ID]```
 
   * Delete a person

 You can delete a person's info from the book
 
 API EndPoint :``` /Persons/Delete?id=[ID]```
 
 * Get all Contacts

You can take all the contact infos with persons in the book

 API EndPoint :``` C/ontacts/GetContact```
 

 * Get Contact by ID

 You can select one contact indo from the list
 
 API EndPoint :```/Contacts/GetContact?id=[contactID]```
 
 * Insert a Contact

 You can add a contact to book
 
 API EndPoint :``` /Contacts/Insert```
 
   * Edit a contact
   
 You can edit a contact's info in the book
 
 API EndPoint :``` /Contacts/Update?id=[ID]```
 
   * Delete a contact

 You can delete a contact's info from the book
 
 API EndPoint :``` /Contacts/Delete?id=[ID]```


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
