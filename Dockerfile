FROM node:lts-alpine3.22

ARG NODE_ENV=development
ENV NODE_ENV=${NODE_ENV}

RUN mkdir /workspace && mkdir /workspace/node_modules && mkdir /workspace/node_modules/.cache && chown -R node:node /workspace/node_modules/.cache

WORKDIR /workspace

COPY . /workspace

RUN apk --no-cache --virtual build-dependencies add yarn
RUN apk add dotnet9-sdk

RUN npm upgrade -g yarn 

RUN npm install -g @angular-devkit/build-angular 
RUN npm install -g @angular/cli

# RUN npm install
RUN yarn install

EXPOSE 8080
EXPOSE 8082
EXPOSE 8084

# CMD ["ng", "serve", "--host", "0.0.0.0"]
#ENTRYPOINT ["start.sh"]
#ENTRYPOINT ["exec", "tail", "-f", "/dev/null"]


