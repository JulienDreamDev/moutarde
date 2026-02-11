# 〽️ Moutarde - Backend

This backend is run from the repository root. See the main instructions in [README.md](../README.md).

To run only the backend and database services:
```bash
docker compose up --build backend database
```

To stop those services:
```bash
docker compose down
```

## Tests

Run tests from this directory (not the repository root):
```bash
dotnet test
```

## Documentation

The API documentation is available via Swagger at:

- http://localhost:5181/swagger