EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'PwdCheckMinLengthEnabled',
	@Value						= 'Y'

EXECUTE spGlobalDataAddIntegerValue
	@KeyName					= 'PwdCheckMinLength',
	@Value						= 6

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'PwdCheckMaxLengthEnabled',
	@Value						= 'Y'

EXECUTE spGlobalDataAddIntegerValue
	@KeyName					= 'PwdCheckMaxLength',
	@Value						= 20

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'PwdCheckMixedCaseEnabled',
	@Value						= 'N'

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'PwdCheckNeedsDigitEnabled',
	@Value						= 'N'

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'PwdCheckSpecialCharacterEnabled',
	@Value						= 'N'

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'PwdCheckNeedsReusedEnabled',
	@Value						= 'Y'

EXECUTE spGlobalDataAddIntegerValue
	@KeyName					= 'PwdCheckReusedNumber',
	@Value						= 20

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'SMTPServer',
	@Value						= 'smtp.test.com'

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'MailFromAddress',
	@Value						= 'test@axinteractive.com'

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'MailFromUsername',
	@Value						= ''

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'MailFromPassword',
	@Value						= ''

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'SiteBaseURL',
	@Value						= ''

EXECUTE spGlobalDataAddStringValue
	@KeyName					= 'FileStoreFolder',
	@Value						= 'C:\Filestore\sedogo'

EXECUTE spGlobalDataAddIntegerValue
	@KeyName					= 'ThumbnailSize',
	@Value						= 50

EXECUTE spGlobalDataAddIntegerValue
	@KeyName					= 'PreviewSize',
	@Value						= 200

GO 