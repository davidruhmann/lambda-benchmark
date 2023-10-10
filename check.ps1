$ErrorActionPreference = 'Stop'
Set-PSDebug -Trace 1

cargo check --workspace --all-targets
cargo check --workspace --all-features
cargo fmt --all -- --check
cargo clippy --workspace --all-targets --all-features --  -D warnings -W clippy::all
cargo test --workspace --all-targets --all-features
