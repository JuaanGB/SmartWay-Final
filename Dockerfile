FROM node:18.19.1-alpine3.19
WORKDIR /app
COPY ./CyberPulseLabs /app
RUN npm install
EXPOSE 8000
CMD ["npm", "run", "dev"]
