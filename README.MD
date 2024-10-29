# .NET TODO List API by Bilguut Vyachyeslav

## Overview

This TODO List API is a minimalistic implementation of a task management API, designed to use a JSON file as a simple database substitute. While it does not use a relational or NoSQL database, the JSON file provides similar functionality for demonstration purposes, allowing us to perform CRUD operations on tasks.

### Design and Structure

To keep the design clean and organized, the API is structured into two layers:

- **Controller Layer**: Responsible for handling API requests, validating input, and forwarding requests to the repository.
- **Repository Layer**: Contains the actual business logic and data operations, implementing a simple repository pattern for separation of concerns. This layer interacts with the JSON "database" to perform CRUD operations.

This layered approach ensures that each component has a clear responsibility, enhancing maintainability and testability.

## Key Technologies

- **Microsoft.Extensions.DependencyInjection** for dependency injection
- **Microsoft.AspNetCore** for building a RESTful API
- **Swagger** for a user-friendly interface to document and test the API
- **System.Text.Json** for JSON serialization and deserialization
- **XUnit** for unit testing
- **NSubstitute** for mocking dependencies in unit tests

## Swagger Documentation

The API is documented and tested via Swagger, providing an interactive UI that displays all available endpoints and allows for simple manual testing. This feature streamlines the testing process and makes the API more accessible to developers.

## Unit Testing

Comprehensive unit tests are written for both the controller and repository layers, ensuring the functionality is robust and covers various cases. `XUnit` is used for test execution, with `NSubstitute` to mock interfaces, allowing for isolated tests that validate each component independently.

## Contact

For any questions or feedback, feel free to reach out:

- **Email**: bilguutvyachyeslav@gmail.com
- **Phone**: 641-819-1633
