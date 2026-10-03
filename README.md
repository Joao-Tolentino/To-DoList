# To-DoList

[![License: AGPL v3](https://img.shields.io/badge/License-AGPL_v3-blue.svg)](LICENSE)
[![Platform: Windows](https://img.shields.io/badge/Platform-Windows-0078D6.svg?logo=windows&logoColor=white)](#)

A console-based task management application in C# that lets users keep track of tasks, update their priorities, due dates, descriptions, and save everything locally using JSON.

---

## Features

- **Local Storage**: Automatically saves and loads your to-do entries from `data.json` so you never lose your progress.
- **Detailed Entries**: Each task supports a Title, Description, Priority (Low, Medium, High), and Due Date.
- **Interactive Console Menu**: Create, toggle completion, edit details, and delete entries seamlessly using an intuitive command-line loop.

---

## Quick Start

1. Clone or download the repository.
2. Ensure you have the .NET SDK installed (e.g., .NET 10).
3. Open your terminal in the project directory and run `dotnet run`.

---

## Configuration Details

No external APIs or complex configurations are needed. The application automatically creates a `data.json` file in the execution directory to serialize the `Entry` objects.

---

## Usage Guidelines

- Run the application.
- Select from 7 menu options:
  1. Add Entry
  2. Toggle Entry Completion
  3. Change Entry Description
  4. Change Entry Priority 
  5. Change Entry Due Date
  6. Delete an Entry
  7. Exit and Save
- Input dates in `DD/MM/YYYY` format and priority as `Low`, `Medium`, or `High`.

---

## Technical Documentation

For developers interested in directory structures, code architecture, or compilation guidelines, please refer to the **[Documentation.md](Documentation.md)** file.

---

## License

This project is licensed under the **GNU Affero General Public License Version 3 (AGPLv3)**. See the LICENSE file for details.
