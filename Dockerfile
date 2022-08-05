# Builder
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS builder

WORKDIR /source

RUN apt-get update && \
   apt-get install -y -q --no-install-recommends \
   ca-certificates=20211016~20.04.1 \
   rsync=3.1.3-8ubuntu0.3 && \
   apt-get clean && \
   rm -r /var/lib/apt/lists/*

COPY . .

RUN rm -rf ModernUO && \
   git submodule update --init --recursive && \
   dotnet publish -r linux-x64 -c Release

# Server
FROM ubuntu:focal

# Used for group and user names
ARG USER=zulu
ARG UID=1000

RUN groupadd -g ${UID} ${USER} && \
    useradd -l -u ${UID} -g ${USER} -s /bin/sh ${USER}

RUN apt-get update && \
   apt-get install -y -q --no-install-recommends \
   ca-certificates=20211016~20.04.1 \
   libargon2-1=0~20171227-0.2 \
   libargon2-dev=0~20171227-0.2 \
   zlib1g=1:1.2.11.dfsg-2ubuntu1.3 \
   zlib1g-dev=1:1.2.11.dfsg-2ubuntu1.3 \
   libicu66=66.1-2ubuntu2.1 \
   libicu-dev=66.1-2ubuntu2.1 \
   zstd=1.4.4+dfsg-3ubuntu0.1 \
   && apt-get clean \
   && rm -r /var/lib/apt/lists/*

COPY --chown=${USER} --from=builder /source/ModernUO/Distribution /app

USER ${USER}
WORKDIR /app
EXPOSE 2593

CMD ["./ModernUO"]
