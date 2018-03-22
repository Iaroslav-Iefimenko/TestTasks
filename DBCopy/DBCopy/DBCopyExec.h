#pragma once
#include "stdafx.h"

class DBCopyExec
{
public:
	DBCopyExec(void);
	~DBCopyExec(void);

	bool ExecuteDBTransfer(_bstr_t sourceConnStr, _bstr_t destConnStr);
private:
	ADO::_ConnectionPtr m_pSource;
	ADO::_ConnectionPtr m_pDest;
};

