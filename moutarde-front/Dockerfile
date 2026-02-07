FROM node:25.6-alpine

WORKDIR /app

COPY package.json yarn.lock .

RUN yarn install

COPY . .

EXPOSE 5180

CMD ["yarn", "dev"]