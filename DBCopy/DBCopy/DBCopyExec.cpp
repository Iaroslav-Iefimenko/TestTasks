#include "stdafx.h"
#include "DBCopyExec.h"


DBCopyExec::DBCopyExec(void)
{
}


DBCopyExec::~DBCopyExec(void)
{
}

bool DBCopyExec::ExecuteDBTransfer(_bstr_t sourceConnStr, _bstr_t destConnStr)
{
	m_pSource.CreateInstance(__uuidof(ADO::Connection));
	m_pSource->Open(sourceConnStr,"","", ADO::ConnectOptionEnum::adConnectUnspecified);
	_variant_t recordsAffected;
	m_pSource->Execute("select * from sys.tables where type_desc = 'USER_TABLE'",&recordsAffected,ADO::ExecuteOptionEnum::adOptionUnspecified);
	return true;
}
