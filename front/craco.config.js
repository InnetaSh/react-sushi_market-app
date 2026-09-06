const path = require("path");

module.exports = {
  webpack: {
    alias: {
      "@": path.resolve(__dirname, "src"),
      "@app": path.resolve(__dirname, "src/app"),
      "@fonts": path.resolve(__dirname, "src/assets/fonts"),
      "@colors": path.resolve(__dirname, "src/assets/styles/abstracts/variables"),
      "@styles": path.resolve(__dirname, "src/assets/styles"),
      "@img": path.resolve(__dirname, "src/assets/images"),
      "@api": path.resolve(__dirname, "src/api"),
      "@section": path.resolve(__dirname, "src/components/sections"),
      "@pages": path.resolve(__dirname, "src/pages"),
      "@layout": path.resolve(__dirname, "src/components/layout"),
      "@stores": path.resolve(__dirname, "src/stores"),
      "@routes": path.resolve(__dirname, "src/routes"),
      "@models": path.resolve(__dirname, "src/models"),
      "@UI": path.resolve(__dirname, "src/components/UI"),
      "@mocks":path.resolve(__dirname,"src/mocks"),
      "@hooks":path.resolve(__dirname,"src/hooks"),
      "@constants": path.resolve(__dirname, "src/constants"),
      "@utils": path.resolve(__dirname, "src/utils")

  
    },
  },
};