import React from "react";
import Uploady from "@rpldy/uploady";
import UploadButton from "@rpldy/upload-button";
import UploadPreview from "@rpldy/upload-preview";

const filterBySize = (file) => {
  return file.size <= 5242880;
};

const ImageUpload = () => (
  <Uploady
    destination={{ url: "my-server.com/upload" }}
    fileFilter={filterBySize}
    accept="image/*"
  >
    <UploadButton />
    <UploadPreview />   
  </Uploady>
);