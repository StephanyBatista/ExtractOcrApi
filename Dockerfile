FROM microsoft/dotnet:2.1-sdk-alpine AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime

# RUN apt-get update && apt-get install -y python-dev libxml2-dev libxslt1-dev antiword unrtf poppler-utils pstotext tesseract-ocr \
#     flac ffmpeg lame libmad0 libsox-fmt-mp3 sox libjpeg-dev swig python-pip zlib1g-dev libpulse-dev

# RUN apk update && apk add python3-dev tesseract-ocr g++  && \
#     apk add --no-cache --update python3 && \
#     pip3 install --upgrade pip setuptools

RUN apt-get update && apt-get install -y python-dev tesseract-ocr g++ python-pip
    
RUN pip install https://github.com/goulu/pdfminer/zipball/e6ad15af79a26c31f4e384d8427b375c93b03533#egg=pdfminer.six

RUN pip install docx2txt

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ExtractOcrApi.dll"]