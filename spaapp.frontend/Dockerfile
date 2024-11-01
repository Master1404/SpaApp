
FROM node:18 AS build

WORKDIR /app

COPY Spaapp.Frontend/package.json ./Spaapp.Frontend/

RUN npm install --prefix ./Spaapp.Frontend

COPY Spaapp.Frontend/ ./Spaapp.Frontend/

RUN npm run build --prefix ./Spaapp.Frontend

FROM nginx:alpine

COPY --from=build /app/Spaapp.Frontend/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]