# Contact List CLI

This is a simple Command Line Interface (CLI) application for managing a contact list. It allows users to add, remove, list, search, and organize contacts. The contacts are stored in a JSON file (`contacts.json`), and the program can persist contacts between runs.

## Features

- **Add Contact**: Allows you to add a new contact with a name, phone number, and/or email address.
- **Remove Contact**: Enables you to remove an existing contact by selecting it from the list.
- **List Contacts**: Displays all contacts with their name, phone number, and email.
- **Search Contact**: Allows searching for a contact by name using binary search.
- **Persistent Storage**: Contacts are saved in a JSON file, and changes persist across program executions.
- **Sort Contacts**: Contacts are sorted by name automatically.

## Requirements

- .NET 6.0 or later

## Installation

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/yourusername/contact-list-cli.git
   ```

2. Navigate to the project directory:

   ```bash
   cd contact-list-cli
   ```

3. Open the project in Visual Studio or another C# IDE.

4. Build and run the application.

## Usage

### Main Menu Options

1. **Add Contact**:

   - You will be prompted to enter a name, phone number, and/or email address for the new contact.

2. **Remove Contact**:

   - You can remove a contact by selecting the index of the contact in the list.

3. **List Contacts**:

   - Displays all the contacts saved in the contact list with their names, phone numbers, and emails.

4. **Search Contact**:

   - Allows you to search for a contact by name using a binary search algorithm.

5. **Exit**:
   - Exits the program.

## How It Works

The application uses a `Contact` class with three properties:

- `Name` (string?)
- `PhoneNumber` (string?)
- `Email` (string?)

Contacts are stored in a list, and the list is saved and loaded from a `contacts.json` file using the `System.Text.Json` namespace. The contacts are sorted alphabetically by name each time the program is run.

The main logic is implemented in the `ContactListCLI` class, which handles all user interactions, including adding, removing, listing, and searching contacts.

## Contributing

If you would like to contribute to this project, feel free to fork the repository, make changes, and submit a pull request. Contributions are always welcome!
