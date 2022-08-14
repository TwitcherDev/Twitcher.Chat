namespace Twitcher.Chat;

/// <summary>List of twitch notice message ids</summary>
public static class NoticeMessageIds
{
    /// <summary>{user} is already banned in this channel</summary>
    public static string AlreadyBanned => "already_banned";

    /// <summary>This room is not in emote-only mode</summary>
    public static string AlreadyEmoteOnlyOff => "already_emote_only_off";

    /// <summary>This room is already in emote-only mode</summary>
    public static string AlreadyEmoteOnlyOn => "already_emote_only_on";

    /// <summary>This room is not in followers-only mode</summary>
    public static string AlreadyFollowersOff => "already_followers_off";

    /// <summary>This room is already in {duration} followers-only mode</summary>
    public static string AlreadyFollowersOn => "already_followers_on";

    /// <summary>This room is not in unique-chat mode</summary>
    public static string AlreadyR9kOff => "already_r9k_off";

    /// <summary>This room is already in unique-chat mode</summary>
    public static string AlreadyR9kOn => "already_r9k_on";

    /// <summary>This room is not in slow mode</summary>
    public static string AlreadySlowOff => "already_slow_off";

    /// <summary>This room is already in {duration}-second slow mode</summary>
    public static string AlreadySlowOn => "already_slow_on";

    /// <summary>This room is not in subscribers-only mode</summary>
    public static string AlreadySubsOff => "already_subs_off";

    /// <summary>This room is already in subscribers-only mode</summary>
    public static string AlreadySubsOn => "already_subs_on";

    /// <summary>{user} is now auto hosting you for up to {number} viewers</summary>
    public static string AutohostReceive => "autohost_receive";

    /// <summary>You cannot ban admin {user}. Please email support@twitch.tv if an admin is being abusive</summary>
    public static string BadBanAdmin => "bad_ban_admin";

    /// <summary>You cannot ban anonymous users</summary>
    public static string BadBanAnon => "bad_ban_anon";

    /// <summary>You cannot ban the broadcaster</summary>
    public static string BadBanBroadcaster => "bad_ban_broadcaster";

    /// <summary>You cannot ban moderator {user} unless you are the owner of this channel</summary>
    public static string BadBanMod => "bad_ban_mod";

    /// <summary>You cannot ban yourself</summary>
    public static string BadBanSelf => "bad_ban_self";

    /// <summary>You cannot ban a staff {user}. Please email support@twitch.tv if a staff member is being abusive</summary>
    public static string BadBanStaff => "bad_ban_staff";

    /// <summary>Failed to start the commercial</summary>
    public static string BadCommercialError => "bad_commercial_error";

    /// <summary>You cannot delete the broadcaster’s messages</summary>
    public static string BadDeleteMessageBroadcaster => "bad_delete_message_broadcaster";

    /// <summary>You cannot delete messages from another moderator {user}</summary>
    public static string BadDeleteMessageMod => "bad_delete_message_mod";

    /// <summary>There was a problem hosting {channel}. Please try again in a minute</summary>
    public static string BadHostError => "bad_host_error";

    /// <summary>This channel is already hosting {channel}</summary>
    public static string BadHostHosting => "bad_host_hosting";

    /// <summary>Host target cannot be changed more than {number} times every half hour</summary>
    public static string BadHostRateExceeded => "bad_host_rate_exceeded";

    /// <summary>This channel is unable to be hosted</summary>
    public static string BadHostRejected => "bad_host_rejected";

    /// <summary>A channel cannot host itself</summary>
    public static string BadHostSelf => "bad_host_self";

    /// <summary>{user} is banned in this channel. You must unban this user before granting mod status</summary>
    public static string BadModBanned => "bad_mod_banned";

    /// <summary>{user} is already a moderator of this channel</summary>
    public static string BadModMod => "bad_mod_mod";

    /// <summary>You cannot set slow delay to more than {number} seconds</summary>
    public static string BadSlowDuration => "bad_slow_duration";

    /// <summary>You cannot timeout admin {user}. Please email support@twitch.tv if an admin is being abusive</summary>
    public static string BadTimeoutAdmin => "bad_timeout_admin";

    /// <summary>You cannot timeout anonymous users</summary>
    public static string BadTimeoutAnon => "bad_timeout_anon";

    /// <summary>You cannot timeout the broadcaster</summary>
    public static string BadTimeoutBroadcaster => "bad_timeout_broadcaster";

    /// <summary>You cannot time a user out for more than {seconds}</summary>
    public static string BadTimeoutDuration => "bad_timeout_duration";

    /// <summary>You cannot timeout moderator {user} unless you are the owner of this channel</summary>
    public static string BadTimeoutMod => "bad_timeout_mod";

    /// <summary>You cannot timeout yourself</summary>
    public static string BadTimeoutSelf => "bad_timeout_self";

    /// <summary>You cannot timeout staff {user}. Please email support@twitch.tv if a staff member is being abusive</summary>
    public static string BadTimeoutStaff => "bad_timeout_staff";

    /// <summary>{user} is not banned from this channel</summary>
    public static string BadUnbanNoBan => "bad_unban_no_ban";

    /// <summary>There was a problem exiting host mode. Please try again in a minute</summary>
    public static string BadUnhostError => "bad_unhost_error";

    /// <summary>{user} is not a moderator of this channel</summary>
    public static string BadUnmodMod => "bad_unmod_mod";

    /// <summary>{user} is banned in this channel. You must unban this user before granting VIP status</summary>
    public static string BadVipGranteeBanned => "bad_vip_grantee_banned";

    /// <summary>{user} is already a VIP of this channel</summary>
    public static string BadVipGranteeAlreadyVip => "bad_vip_grantee_already_vip";

    /// <summary>Unable to add VIP. Visit the Achievements page on your dashboard to learn how to unlock additional VIP slots</summary>
    public static string BadVipMaxVipsReached => "bad_vip_max_vips_reached";

    /// <summary>Unable to add VIP. Visit the Achievements page on your dashboard to learn how to unlock this feature</summary>
    public static string BadVipAchievementIncomplete => "bad_vip_achievement_incomplete";

    /// <summary>{user} is not a VIP of this channel</summary>
    public static string BadUnvipGranteeNotVip => "bad_unvip_grantee_not_vip";

    /// <summary>{user} is now banned from this channel</summary>
    public static string BanSuccess => "ban_success";

    /// <summary>Commands available to you in this room (use /help for details): {list of commands} More help: https://help.twitch.tv/s/article/chat-commands</summary>
    public static string CmdsAvailable => "cmds_available";

    /// <summary>Your color has been changed</summary>
    public static string ColorChanged => "color_changed";

    /// <summary>Initiating {number} second commercial break. Keep in mind that your stream is still live and not everyone will get a commercial</summary>
    public static string CommercialSuccess => "commercial_success";

    /// <summary>The message from {user} is now deleted</summary>
    public static string DeleteMessageSuccess => "delete_message_success";

    /// <summary>You deleted a message from staff {user}. Please email support@twitch.tv if a staff member is being abusive</summary>
    public static string DeleteStaffMessageSuccess => "delete_staff_message_success";

    /// <summary>This room is no longer in emote-only mode</summary>
    public static string EmoteOnlyOff => "emote_only_off";

    /// <summary>This room is now in emote-only mode</summary>
    public static string EmoteOnlyOn => "emote_only_on";

    /// <summary>This room is no longer in followers-only mode</summary>
    public static string FollowersOff => "followers_off";

    /// <summary>This room is now in {duration} followers-only mode</summary>
    public static string FollowersOn => "followers_on";

    /// <summary>This room is now in followers-only mode</summary>
    public static string FollowersOnZero => "followers_on_zero";

    /// <summary>Exited host mode</summary>
    public static string HostOff => "host_off";

    /// <summary>Now hosting {channel}</summary>
    public static string HostOn => "host_on";

    /// <summary>{channel} is now hosting you for up to {number} viewers</summary>
    public static string HostReceive => "host_receive";

    /// <summary>{channel} is now hosting you</summary>
    public static string HostReceiveNoCount => "host_receive_no_count";

    /// <summary>{channel} has gone offline. Exiting host mode</summary>
    public static string HostTargetWentOffline => "host_target_went_offline";

    /// <summary>{number} host commands remaining this half hour</summary>
    public static string HostsRemaining => "hosts_remaining";

    /// <summary>Invalid username: {user}</summary>
    public static string InvalidUser => "invalid_user";

    /// <summary>You have added {user} as a moderator of this channel</summary>
    public static string ModSuccess => "mod_success";

    /// <summary>You are permanently banned from talking in {channel}</summary>
    public static string MsgBanned => "msg_banned";

    /// <summary>Your message was not sent because it contained too many unprocessable characters. If you believe this is an error, please rephrase and try again</summary>
    public static string MsgBadCharacters => "msg_bad_characters";

    /// <summary>Your message was not sent because your account is not in good standing in this channel</summary>
    public static string MsgChannelBlocked => "msg_channel_blocked";

    /// <summary>This channel does not exist or has been suspended</summary>
    public static string MsgChannelSuspended => "msg_channel_suspended";

    /// <summary>Your message was not sent because it is identical to the previous one you sent, less than 30 seconds ago</summary>
    public static string MsgDuplicate => "msg_duplicate";

    /// <summary>This room is in emote-only mode. You can find your currently available emoticons using the smiley in the chat text area</summary>
    public static string MsgEmoteonly => "msg_emoteonly";

    /// <summary>This room is in {duration} followers-only mode. Follow {channel} to join the community! Note: These msg_followers tags are kickbacks to a user who does not meet the criteria; that is, does not follow or has not followed long enough</summary>
    public static string MsgFollowersonly => "msg_followersonly";

    /// <summary>This room is in {duration1} followers-only mode. You have been following for {duration2}. Continue following to chat!</summary>
    public static string MsgFollowersonlyFollowed => "msg_followersonly_followed";

    /// <summary>This room is in followers-only mode. Follow {channel} to join the community!</summary>
    public static string MsgFollowersonlyZero => "msg_followersonly_zero";

    /// <summary>This room is in unique-chat mode and the message you attempted to send is not unique</summary>
    public static string MsgR9k => "msg_r9k";

    /// <summary>Your message was not sent because you are sending messages too quickly</summary>
    public static string MsgRatelimit => "msg_ratelimit";

    /// <summary>Hey! Your message is being checked by mods and has not been sent</summary>
    public static string MsgRejected => "msg_rejected";

    /// <summary>Your message wasn’t posted due to conflicts with the channel’s moderation settings</summary>
    public static string MsgRejectedMandatory => "msg_rejected_mandatory";

    /// <summary>A verified phone number is required to chat in this channel. Please visit https://www.twitch.tv/settings/security to verify your phone number</summary>
    public static string MsgRequiresVerifiedPhoneNumber => "msg_requires_verified_phone_number";

    /// <summary>This room is in slow mode and you are sending messages too quickly. You will be able to talk again in {number} seconds</summary>
    public static string MsgSlowmode => "msg_slowmode";

    /// <summary>This room is in subscribers only mode. To talk, purchase a channel subscription at https://www.twitch.tv/products/{broadcaster login name}/ticket?ref=subscriber_only_mode_chat</summary>
    public static string MsgSubsonly => "msg_subsonly";

    /// <summary>You don’t have permission to perform that action</summary>
    public static string MsgSuspended => "msg_suspended";

    /// <summary>You are timed out for {number} more seconds</summary>
    public static string MsgTimedout => "msg_timedout";

    /// <summary>This room requires a verified account to chat. Please verify your account at https://www.twitch.tv/settings/security</summary>
    public static string MsgVerifiedEmail => "msg_verified_email";

    /// <summary>No help available</summary>
    public static string NoHelp => "no_help";

    /// <summary>There are no moderators of this channel</summary>
    public static string NoMods => "no_mods";

    /// <summary>This channel does not have any VIPs</summary>
    public static string NoVips => "no_vips";

    /// <summary>No channel is currently being hosted</summary>
    public static string NotHosting => "not_hosting";

    /// <summary>You don’t have permission to perform that action</summary>
    public static string NoPermission => "no_permission";

    /// <summary>This room is no longer in unique-chat mode</summary>
    public static string R9kOff => "r9k_off";

    /// <summary>This room is now in unique-chat mode</summary>
    public static string R9kOn => "r9k_on";

    /// <summary>You already have a raid in progress</summary>
    public static string RaidErrorAlreadyRaiding => "raid_error_already_raiding";

    /// <summary>You cannot raid this channel</summary>
    public static string RaidErrorForbidden => "raid_error_forbidden";

    /// <summary>A channel cannot raid itself</summary>
    public static string RaidErrorSelf => "raid_error_self";

    /// <summary>Sorry, you have more viewers than the maximum currently supported by raids right now</summary>
    public static string RaidErrorTooManyViewers => "raid_error_too_many_viewers";

    /// <summary>There was a problem raiding {channel}. Please try again in a minute</summary>
    public static string RaidErrorUnexpected => "raid_error_unexpected";

    /// <summary>This channel is intended for mature audiences</summary>
    public static string RaidNoticeMature => "raid_notice_mature";

    /// <summary>This channel has follower- or subscriber-only chat</summary>
    public static string RaidNoticeRestrictedChat => "raid_notice_restricted_chat";

    /// <summary>The moderators of this channel are: {list of users}</summary>
    public static string RoomMods => "room_mods";

    /// <summary>This room is no longer in slow mode</summary>
    public static string SlowOff => "slow_off";

    /// <summary>This room is now in slow mode. You may send messages every {number} seconds</summary>
    public static string SlowOn => "slow_on";

    /// <summary>This room is no longer in subscribers-only mode</summary>
    public static string SubsOff => "subs_off";

    /// <summary>This room is now in subscribers-only mode</summary>
    public static string SubsOn => "subs_on";

    /// <summary>{user} is not timed out from this channel</summary>
    public static string TimeoutNoTimeout => "timeout_no_timeout";

    /// <summary>{user} has been timed out for {duration}</summary>
    public static string TimeoutSuccess => "timeout_success";

    /// <summary>The community has closed channel {channel} due to Terms of Service violations</summary>
    public static string TosBan => "tos_ban";

    /// <summary>Only turbo users can specify an arbitrary hex color. Use one of the following instead: {list of colors}</summary>
    public static string TurboOnlyColor => "turbo_only_color";

    /// <summary>Sorry, “{command}” is not available through this client</summary>
    public static string UnavailableCommand => "unavailable_command";

    /// <summary>{user} is no longer banned from this channel</summary>
    public static string UnbanSuccess => "unban_success";

    /// <summary>You have removed {user} as a moderator of this channel</summary>
    public static string UnmodSuccess => "unmod_success";

    /// <summary>You do not have an active raid</summary>
    public static string UnraidErrorNoActiveRaid => "unraid_error_no_active_raid";

    /// <summary>There was a problem stopping the raid. Please try again in a minute</summary>
    public static string UnraidErrorUnexpected => "unraid_error_unexpected";

    /// <summary>The raid has been canceled</summary>
    public static string UnraidSuccess => "unraid_success";

    /// <summary>Unrecognized command: {command}</summary>
    public static string UnrecognizedCmd => "unrecognized_cmd";

    /// <summary>{user} is permanently banned. Use “/unban” to remove a ban</summary>
    public static string UntimeoutBanned => "untimeout_banned";

    /// <summary>{user} is no longer timed out in this channel</summary>
    public static string UntimeoutSuccess => "untimeout_success";

    /// <summary>You have removed {user} as a VIP of this channel</summary>
    public static string UnvipSuccess => "unvip_success";

    /// <summary>Usage: “/ban {username} [reason]” Permanently prevent a user from chatting. Reason is optional and will be shown to the target and other moderators. Use “/unban” to remove a ban</summary>
    public static string UsageBan => "usage_ban";

    /// <summary>Usage: “/clear” - Clear chat history for all users in this room</summary>
    public static string UsageClear => "usage_clear";

    /// <summary>Usage: “/color” {color} - Change your username color. Color must be in hex (#000000) or one of the following: {list of colors}</summary>
    public static string UsageColor => "usage_color";

    /// <summary>Usage: “/commercial [length]” - Triggers a commercial. Length (optional) must be a positive number of seconds</summary>
    public static string UsageCommercial => "usage_commercial";

    /// <summary>Usage: “/disconnect” - Reconnects to chat</summary>
    public static string UsageDisconnect => "usage_disconnect";

    /// <summary>Usage: “/delete {msg id}” - Deletes the specified message. For more information, see https://dev.twitch.tv/docs/irc/commands/#clearmsg-twitch-commands</summary>
    public static string UsageDelete => "usage_delete";

    /// <summary>Usage: /emoteonlyoff” - Disables emote-only mode</summary>
    public static string UsageEmoteOnlyOff => "usage_emote_only_off";

    /// <summary>Usage: “/emoteonly” - Enables emote-only mode (only emoticons may be used in chat). Use /emoteonlyoff to disable</summary>
    public static string UsageEmoteOnlyOn => "usage_emote_only_on";

    /// <summary>Usage: /followersoff” - Disables followers-only mode</summary>
    public static string UsageFollowersOff => "usage_followers_off";

    /// <summary>Usage: “/followers - Enables followers-only mode (only users who have followed for “duration” may chat). Examples: “30m”, “1 week”, “5 days 12 hours”. Must be less than 3 months</summary>
    public static string UsageFollowersOn => "usage_followers_on";

    /// <summary>Usage: “/help” - Lists the commands available to you in this room</summary>
    public static string UsageHelp => "usage_help";

    /// <summary>Usage: “/host {channel}“ - Host another channel. Use “/unhost” to unset host mode</summary>
    public static string UsageHost => "usage_host";

    /// <summary>Usage: “/marker {optional comment}“ - Adds a stream marker (with an optional comment, max 140 characters) at the current timestamp. You can use markers in the Highlighter for easier editing</summary>
    public static string UsageMarker => "usage_marker";

    /// <summary>Usage: “/me {message}” - Express an action in the third-person</summary>
    public static string UsageMe => "usage_me";

    /// <summary>Usage: “/mod {username}” - Grant moderator status to a user. Use “/mods” to list the moderators of this channel</summary>
    public static string UsageMod => "usage_mod";

    /// <summary>Usage: “/mods” - Lists the moderators of this channel</summary>
    public static string UsageMods => "usage_mods";

    /// <summary>Usage: “/uniquechatoff” - Disables unique-chat mode</summary>
    public static string UsageR9kOff => "usage_r9k_off";

    /// <summary>Usage: “/uniquechat” - Enables unique-chat mode. Use “/uniquechatoff” to disable</summary>
    public static string UsageR9kOn => "usage_r9k_on";

    /// <summary>Usage: “/raid {channel}“ - Raid another channel. - Use “/unraid” to cancel the Raid</summary>
    public static string UsageRaid => "usage_raid";

    /// <summary>Usage: “/slowoff” - Disables slow mode</summary>
    public static string UsageSlowOff => "usage_slow_off";

    /// <summary>Usage: “/slow” [duration] - Enables slow mode (limit how often users may send messages). Duration (optional, default={number}) must be a positive integer number of seconds. - Use “/slowoff” to disable</summary>
    public static string UsageSlowOn => "usage_slow_on";

    /// <summary>Usage: “/subscribersoff” - Disables subscribers-only mode</summary>
    public static string UsageSubsOff => "usage_subs_off";

    /// <summary>Usage: “/subscribers” - Enables subscribers-only mode (only subscribers may chat in this channel). - Use “/subscribersoff” to disable</summary>
    public static string UsageSubsOn => "usage_subs_on";

    /// <summary>Usage: “/timeout {username} [duration][time unit] [reason]” - Temporarily prevent a user from chatting. Duration (optional, default=10 minutes) must be a positive integer; time unit (optional, default=s) must be one of s, m, h, d, w; maximum duration is 2 weeks. Combinations like 1d2h are also allowed. Reason is optional and will be shown to the target user and other moderators. - Use “untimeout” to remove a timeout</summary>
    public static string UsageTimeout => "usage_timeout";

    /// <summary>Usage: “/unban {username}“ - Removes a ban on a user</summary>
    public static string UsageUnban => "usage_unban";

    /// <summary>Usage: “/unhost” - Stop hosting another channel</summary>
    public static string UsageUnhost => "usage_unhost";

    /// <summary>Usage: “/unmod {username}” - Revoke moderator status from a user. Use “/mods” to list the moderators of this channel</summary>
    public static string UsageUnmod => "usage_unmod";

    /// <summary>Usage: “/unraid” - Cancel the Raid</summary>
    public static string UsageUnraid => "usage_unraid";

    /// <summary>Usage: “/untimeout {username}“ - Removes a timeout on a user</summary>
    public static string UsageUntimeout => "usage_untimeout";

    /// <summary>Usage: “/unvip {username}” - Revoke VIP status from a user. Use “/vips” to list the VIPs of this channel</summary>
    public static string UsageUnvip => "usage_unvip";

    /// <summary>Usage: “/user” {username} - Display information about a specific user on this channel</summary>
    public static string UsageUser => "usage_user";

    /// <summary>Usage: “/vip {username}” - Grant VIP status to a user. Use “/vips” to list the VIPs of this channel</summary>
    public static string UsageVip => "usage_vip";

    /// <summary>Usage: “/vips” - Lists the VIPs of this channel</summary>
    public static string UsageVips => "usage_vips";

    /// <summary>Usage: “/w {username} {message}”</summary>
    public static string UsageWhisper => "usage_whisper";

    /// <summary>You have added {user} as a vip of this channel</summary>
    public static string VipSuccess => "vip_success";

    /// <summary>The VIPs of this channel are: {list of users}</summary>
    public static string VipsSuccess => "vips_success";

    /// <summary>You have been banned from sending whispers</summary>
    public static string WhisperBanned => "whisper_banned";

    /// <summary>That user has been banned from receiving whispers</summary>
    public static string WhisperBannedRecipient => "whisper_banned_recipient";

    /// <summary>No user matching that username</summary>
    public static string WhisperInvalidLogin => "whisper_invalid_login";

    /// <summary>You cannot whisper to yourself</summary>
    public static string WhisperInvalidSelf => "whisper_invalid_self";

    /// <summary>You are sending whispers too fast. Try again in a minute</summary>
    public static string WhisperLimitPerMin => "whisper_limit_per_min";

    /// <summary>You are sending whispers too fast. Try again in a second</summary>
    public static string WhisperLimitPerSec => "whisper_limit_per_sec";

    /// <summary>Your settings prevent you from sending this whisper</summary>
    public static string WhisperRestricted => "whisper_restricted";

    /// <summary>That user’s settings prevent them from receiving this whisper.</summary>
    public static string WhisperRestrictedRecipient => "whisper_restricted_recipient";
}
