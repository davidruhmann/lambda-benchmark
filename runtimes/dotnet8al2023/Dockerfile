ARG IMAGE_ARCH
FROM $IMAGE_ARCH/amazonlinux:2023 AS builder
ARG ARCH
WORKDIR /tmp
COPY src .
RUN yum update -y && yum install -y clang zlib-devel krb5-devel openssl-devel zip gzip tar wget
RUN wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh && chmod +x ./dotnet-install.sh && ./dotnet-install.sh --version latest
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
RUN mkdir publish && /root/.dotnet/dotnet publish --configuration Release --arch $ARCH --output publish
RUN zip -j /tmp/code.zip /tmp/publish/bootstrap

FROM scratch
COPY --from=builder /tmp/code.zip /
ENTRYPOINT ["/code.zip"]
