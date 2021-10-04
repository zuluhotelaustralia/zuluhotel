FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS builder

COPY . /source
WORKDIR /source

RUN git submodule update --init --recursive

RUN apt-get update \
   && apt-get install -y -q --no-install-recommends \
   ca-certificates rsync \
   && apt-get clean \
   && rm -r /var/lib/apt/lists/*

RUN dotnet publish -r linux-x64 -c Release

FROM ubuntu:focal

ARG user=zulu
ARG group=zulu
ARG uid=1000
ARG gid=1000


RUN apt-get update \
   && apt-get install -y -q --no-install-recommends \
   ca-certificates libargon2-1 libargon2-dev zlib1g zlib1g-dev libicu66 libicu-dev zstd \
   && apt-get clean \
   && rm -r /var/lib/apt/lists/*

RUN groupadd -g ${gid} ${group} && useradd -u ${uid} -g ${group} -s /bin/sh ${user}

USER ${user}

COPY --chown=${user} --from=builder /source/ModernUO/Distribution /app

WORKDIR /app
EXPOSE 2593

CMD ["./ModernUO"]
