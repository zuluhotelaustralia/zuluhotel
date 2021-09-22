#!/usr/bin/env sh

go build -o golibcue.so -buildmode=c-shared main.go

GOOS=windows GOARCH=386 \
  CGO_ENABLED=1 CXX=i686-w64-mingw32-g++ CC=i686-w64-mingw32-gcc \
  go build -o golibcue.dll -buildmode=c-shared main.go
