/* eslint-disable no-fallthrough */
/* eslint-disable default-case */
import { ContactSettings } from "./ContactSettings";
import { PersonalInfoSettings } from "./PersonalInfoSettings";
import { AccountSettings } from "./AccountSettings";
import { ContactSettingsEdit } from "./ContactSettingsEdit";
import { PersonalInfoSettingsEdit } from "./PersonalInfoSettingsEdit";
import { AccountSettingsEdit } from "./AccountSettingsEdit";

import '../../../styles/UserSettings.css'

export function UserSettings({
  layoutState,
  onLayoutChange,
  user,
  accountSettings,
}) {
  return (
    <div className='user-settings'>
      <div className='options'>
        <button className ='option-btn' onClick={() => onLayoutChange("settings-personal-info", "center")}>
          Personal Info
        </button>
        <button className ='option-btn' onClick={() => onLayoutChange("settings-contacts", "center")}>
          Contacts
        </button>
        <button className ='option-btn' onClick={() => onLayoutChange("settings-account", "center")}>
          Account
        </button>
      </div>
      {(() => {
        switch (layoutState) {
          case "settings-personal-info":
            return (
              <PersonalInfoSettings
                user={user}
                onLayoutChange={onLayoutChange}
              />
            );
          case "settings-contacts":
            return (
              <ContactSettings user={user} onLayoutChange={onLayoutChange} />
            );
          case "settings-account":
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
              <AccountSettingsEdit
                user={user}
                onLayoutChange={onLayoutChange}
              />
            );
          case "settings-contacts-edit":
            return (
              <ContactSettingsEdit
                user={user}
                accountSettings={accountSettings}
                onLayoutChange={onLayoutChange}
              />
            );
        }
      })()}
    </div>
  );
}
