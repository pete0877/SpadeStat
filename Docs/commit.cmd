net user /add /y spadestatdb spadestatdb /EXPIRES:NEVER /COMMENT:"SpadeStat DB Service Account" /FULLNAME:"SpadeStat DB Service Account"
ntrights -u spadestatdb +r SeServiceLogonRight
