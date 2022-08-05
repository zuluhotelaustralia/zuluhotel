# Builder
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS builder

WORKDIR /source

RUN apt-get update \
   && apt-get install -y -q --no-install-recommends \
   ca-certificates rsync \
   && apt-get clean \
   && rm -r /var/lib/apt/lists/*

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

USER ${user}

RUN apt-get update && \
   apt-get install -y -q --no-install-recommends \
   ca-certificates \
   libargon2-1 \
   libargon2-dev \
   zlib1g \
   zlib1g-dev \
   libicu66 \
   libicu-dev \
   zstd \
   && apt-get clean \
   && rm -r /var/lib/apt/lists/*

COPY --chown=${USER} --from=builder /source/ModernUO/Distribution /app

WORKDIR /app
EXPOSE 2593

CMD ["./ModernUO"]
