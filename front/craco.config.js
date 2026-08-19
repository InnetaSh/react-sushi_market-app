const path = require("path");

module.exports = {
  webpack: {
    alias: {
      "@": path.resolve(__dirname, "src"),
      "@app": path.resolve(__dirname, "src/app"),
      "@fonts": path.resolve(__dirname, "src/assets/fonts"),
      "@colors": path.resolve(__dirname, "src/assets/variables"),
      "@img": path.resolve(__dirname, "src/img"),
      "@api": path.resolve(__dirname, "src/app/api"),
      "@section": path.resolve(__dirname, "src/components/sections"),
      "@stores": path.resolve(__dirname, "src/app/stores"),
      "@models": path.resolve(__dirname, "src/models"),
      "@UI": path.resolve(__dirname, "src/components/UI"),
    },
  },
};