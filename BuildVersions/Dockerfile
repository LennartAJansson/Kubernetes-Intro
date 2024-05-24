# Stage 0, "build-stage", based on Node.js, to build and compile the frontend
FROM node:latest as build-stage
WORKDIR /app
COPY ./BuildVersions/. .
RUN npm install
ARG Configuration
RUN npm run build -- --output-path=./dist/out --configuration $Configuration
RUN ls -l /app/dist/out/*

# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
FROM nginx:latest
COPY --from=build-stage /app/dist/out/ /usr/share/nginx/html
COPY --from=build-stage /app/nginx-custom.conf /etc/nginx/conf.d/default.conf
