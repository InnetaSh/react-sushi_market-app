# Build stage
FROM node:20-alpine AS build
WORKDIR /app

# Install dependencies with caching
COPY package.json package-lock.json ./
RUN npm install

# Build arguments for React environment variables
ARG REACT_APP_API_URL=http://localhost:5292/api
ARG REACT_APP_GOOGLE_MAPS_API_KEY=AIzaSyBypxcsu2d043hcikCjW-0semHdXSpLuww
ARG REACT_APP_GOOGLE_CLIENT_ID=123721973387-7i3rs06c8iui5lrb805f22o0k2s6gk1o.apps.googleusercontent.com

ENV REACT_APP_API_URL=$REACT_APP_API_URL
ENV REACT_APP_GOOGLE_MAPS_API_KEY=$REACT_APP_GOOGLE_MAPS_API_KEY
ENV REACT_APP_GOOGLE_CLIENT_ID=$REACT_APP_GOOGLE_CLIENT_ID

# Copy application source code and build
COPY . .
RUN npm run build

# Production stage with Nginx
FROM nginx:alpine AS final
COPY --from=build /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

