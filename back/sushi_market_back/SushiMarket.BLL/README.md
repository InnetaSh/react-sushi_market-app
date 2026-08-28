# Sushi Market

## About the project
## Technologies
## Architecture
## Features
## Translation API
## Database
## Docker
## Installation
## Running the project
## Testing

## Translation API

The project uses TranslateAPI for automatic translation of product
and category titles and descriptions.

### Configuration

The API key is not stored in the repository.

For local development, configure the API key using .NET User Secrets:

```bash
dotnet user-secrets set "Translator:ApiKey" "YOUR_API_KEY"