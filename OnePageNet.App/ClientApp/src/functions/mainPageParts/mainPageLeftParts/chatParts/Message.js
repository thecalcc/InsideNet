import React from 'react';
import { useEffect } from 'react';

export function Message(props) {


  return (
    <div>
      {props.message.content}
    </div>
  );
};