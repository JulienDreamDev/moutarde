# 〽️ Moutarde - Mini Social Network

> Full-stack social media platform built with **React** + **ASP.NET Core** + **PostgreSQL**  
> Featuring custom JWT-based Single Sign-On (SSO) implementation

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## 🎯 Project Goals

This project is a learning exercise to:
- Build a production-ready full-stack application
- Implement custom JWT-based SSO from scratch
- Practice modern DevOps (Docker, CI/CD)
- Prepare for technical interviews

## 🛠️ Tech Stack

### Backend
- ASP.NET Core 8 (Web API)
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- xUnit (testing)

### Frontend
- React + TypeScript
- Vite
- TanStack Query (React Query)
- Tailwind CSS
- Vitest (testing)

### DevOps
- Docker + Docker Compose
- GitHub Actions (CI/CD)
- Nginx (reverse proxy)

## How to build / run

### Development (Docker Compose)
```bash
docker compose up --build
```

Services:
- Frontend: http://localhost:5180
- Backend: http://localhost:5181
- Postgres: localhost:5432

Stop:
```bash
docker compose down
```

### Production (Docker Compose)
Create file [.env.prod](.env.prod) with all needed variables before start up:
```bash
POSTGRES_DB=...
POSTGRES_USER=...
POSTGRES_PASSWORD=...
```

```bash
docker compose -f docker-compose.prod.yml --env-file .env.prod up --build
```

Stop:
```bash
docker compose -f docker-compose.prod.yml down
```

## 🚀 Planned Features

- [x] Project setup
- [ ] JWT Authentication (Week 1)
- [ ] Posts CRUD (Week 1)
- [ ] User profiles (Week 2)
- [ ] Follow/Unfollow (Week 2)
- [ ] Likes & Comments (Week 2)
- [ ] Search (Week 2)
- [ ] Custom SSO server (Week 3)
- [ ] Real-time notifications (Week 3)
- [ ] Production deployment (Week 3)

## 📅 Development Timeline

- **Week 1**: Foundation & Authentication
- **Week 2**: Social Features
- **Week 3**: SSO + Production

## 🏗️ Architecture

Todo

## 🚧 Current Status

Follow progress: [GitHub Issues](https://github.com/JulienDreamDev/moutarde/issues)

## 📝 License

MIT

## 👤 Author

**Julien** - [@JulienDreamDev](https://github.com/JulienDreamDev)
