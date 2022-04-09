/* eslint-disable no-fallthrough */
/* eslint-disable default-case */
import { ContactSettings } from "./ContactSettings";
import { PersonalInfoSettings } from "./PersonalInfoSettings";
import { AccountSettings } from "./AccountSettings";

export function UserSettings({
  layoutState,
  onLayoutChange,
  user,
  accountSettings,
}) {
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
