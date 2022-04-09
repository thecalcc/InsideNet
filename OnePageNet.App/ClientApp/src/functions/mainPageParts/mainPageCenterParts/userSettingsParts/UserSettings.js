/* eslint-disable no-fallthrough */
/* eslint-disable default-case */
import { ContactSettings } from "./ContactSettings";
import { PersonalInfoSettings } from "./PersonalInfoSettings";
import { AccountSettings } from "./AccountSettings";
import { ContactSettingsEdit } from "./ContactSettingsEdit";
import { PersonalInfoSettingsEdit } from "./PersonalInfoSettingsEdit";
import { AccountSettingsEdit } from "./AccountSettingsEdit";

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
            return (
              <PersonalInfoSettings
                user={user}
                onLayoutChange={onLayoutChange}
              />
            );
          case "settings-account":
            return (
              <ContactSettings user={user} onLayoutChange={onLayoutChange} />
            );
          case "settings-about":
            return (
              <AccountSettings
                accountSettings={accountSettings}
                onLayoutChange={onLayoutChange}
              />
            );
          case "settings-personal-info-edit":
            return (
              <PersonalInfoSettingsEdit
                user={user}
                onLayoutChange={onLayoutChange}
              />
            );
          case "settings-account-edit":
            return (
              <ContactSettingsEdit
                user={user}
                onLayoutChange={onLayoutChange}
              />
            );
          case "settings-about-edit":
            return (
              <AccountSettingsEdit
                user={user}
                accountSettings={accountSettings}
                onLayoutChange={onLayoutChange}
              />
            );
        }
      })()}
    </>
  );
}
