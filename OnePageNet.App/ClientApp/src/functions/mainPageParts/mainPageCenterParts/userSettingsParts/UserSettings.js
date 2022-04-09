/* eslint-disable no-fallthrough */
/* eslint-disable default-case */
import { ContactSettings } from "./ContactSettings";
import { PersonalInfoSettings } from "./PersonalInfoSettings";
import { AccountSettings } from "./AccountSettings";
import { useEffect, useState } from "react";

export function UserSettings({layoutState, onLayoutChange, }) {
  const [user, setUser] = useState();
  const [accountSettings, setAccountSettings] = useState();

  useEffect(() => {
    const fetchAccountSettings = async () => {
      const url = `https://localhost:7231/api/AccountSettings/get/${sessionStorage.currentUserId}`;
      
      await fetch(url, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((x) => x.json())
        .then((x) => setAccountSettings(x));
    };
    fetchAccountSettings();
    const fetchUser = async () => {
      const url = `https://localhost:7231/api/users/get/${sessionStorage.currentUserId}`;
      
      await fetch(url, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((x) => x.json())
        .then((x) => setUser(x));
    };
    fetchUser();
  }, []);

  return (
    <>
      <button
        onClick={() => onLayoutChange("settings-personal-info", "center")}
      >
        Personal Info
      </button>
      <button onClick={() => onLayoutChange("settings-about", "center")}>
        About Settings
      </button>
      <button onClick={() => onLayoutChange("settings-account", "center")}>
        Account Settings
      </button>
      {(() => {
        switch (layoutState) {
          case "settings-personal-info":
            return <PersonalInfoSettings user={user} />;
          case "settings-account":
            return <ContactSettings user={user} />;
          case "settings-about":
            return <AccountSettings accountSettings={accountSettings} />;
        }
      })()}
    </>
  );
}
