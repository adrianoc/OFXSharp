﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace OFXSharp
{
    #region Account
    public abstract class Account
    {
        public string AccountID { get; set; }
        public AccountType AccountType { get; set; }
        public IList<Transaction> Transactions { get; set; }

        public Account()
        { }

        public abstract void ImportTransactions(OFXDocumentParser parser, OFXDocument ofx, XmlDocument doc);
    }
    #endregion

    #region BankAccount
    public class BankAccount : Account
    {
        #region Bank Only
        public string AccountKey { get; set; }

        public string BankID { get; set; }

        public string BranchID { get; set; }

        public BankAccountType BankAccountType { get; set; }
        #endregion

        public BankAccount(XmlNode node)
        {
            AccountType = AccountType.BANK;

            AccountID = node.GetValue("//ACCTID");
            AccountKey = node.GetValue("//ACCTKEY");
            BankID = node.GetValue("//BANKID");
            BranchID = node.GetValue("//BRANCHID");

            //Get Bank Account Type from XML
            string bankAccountType = node.GetValue("//ACCTTYPE");

            //Check that it has been set
            if (String.IsNullOrEmpty(bankAccountType))
                throw new OFXParseException("Bank Account type unknown");

            //Set bank account enum
            BankAccountType = bankAccountType.GetBankAccountType();

            Transactions = new List<Transaction>();
        }

        /// <summary>
        /// Returns list of all transactions in OFX document
        /// </summary>
        /// <param name="doc">OFX document</param>
        /// <returns>List of transactions found in OFX document</returns>
        public override void ImportTransactions(OFXDocumentParser parser, OFXDocument ofx, XmlDocument doc)
        {
            XmlNodeList transactionNodes = null;
            var xpath = parser.GetXPath(AccountType, OFXDocumentParser.OFXSection.TRANSACTIONS);

            ofx.StatementStart = doc.GetValue(xpath + "//DTSTART").ToDate();
            ofx.StatementEnd = doc.GetValue(xpath + "//DTEND").ToDate();

            transactionNodes = doc.SelectNodes(xpath + "//STMTTRN");

            foreach (XmlNode node in transactionNodes)
                Transactions.Add(new BankTransaction(node, ofx.Currency));
        }
    }
    #endregion

    #region Investment Account
    public class InvestmentAccount : Account
    {
        public string BrokerID { get; set; }

        public IList<StockInfo> StockQuotes { get; set; }
        public IList<StockPosition> StockPositions { get; set; }

        public InvestmentAccount(XmlNode node)
        {
            AccountType = AccountType.INVESTMENT;

            AccountID = node.GetValue("//ACCTID");
            BrokerID = node.GetValue("//BROKERID");

            Transactions = new List<Transaction>();
        }

        /// <summary>
        /// Returns list of all transactions in OFX document
        /// </summary>
        /// <param name="doc">OFX document</param>
        /// <returns>List of transactions found in OFX document</returns>
        public override void ImportTransactions(OFXDocumentParser parser, OFXDocument ofx, XmlDocument doc)
        {
            XmlNodeList transactionNodes = null;
            var xpath = parser.GetXPath(ofx.AccType, OFXDocumentParser.OFXSection.TRANSACTIONS);

            ofx.StatementStart = doc.GetValue(xpath + "//DTSTART").ToDate();
            ofx.StatementEnd = doc.GetValue(xpath + "//DTEND").ToDate();

            //Import Income Transactions
            transactionNodes = doc.SelectNodes(xpath + "//INCOME");
            foreach (XmlNode node in transactionNodes)
                Transactions.Add(new IncomeTransaction(node, ofx.Currency));

            //Import Buy Stock Transactions
            transactionNodes = doc.SelectNodes(xpath + "//BUYSTOCK");
            foreach (XmlNode node in transactionNodes)
                Transactions.Add(new BuySellStockTransaction(node, ofx.Currency));

            //Import Sell Stock Transactions
            transactionNodes = doc.SelectNodes(xpath + "//SELLSTOCK");
            foreach (XmlNode node in transactionNodes)
                Transactions.Add(new BuySellStockTransaction(node, ofx.Currency));
        }

        public void ImportPositions(string xpath, OFXDocument ofx, XmlDocument doc)
        {
            XmlNodeList positionNodes = null;
            StockPositions = new List<StockPosition>();

            //Import Position Transactions
            positionNodes = doc.SelectNodes(xpath + "//POSSTOCK");
            foreach (XmlNode node in positionNodes)
                StockPositions.Add(new StockPosition(node));
        }

        public void ImportSECList(XmlNode doc)
        {
            XmlNodeList quoteNodes = null;
            StockQuotes = new List<StockInfo>();

            //Import Stock Quotes Transactions
            quoteNodes = doc.SelectNodes("//STOCKINFO");
            foreach (XmlNode node in quoteNodes)
                StockQuotes.Add(new StockInfo(node));

        }
    }
    #endregion

    #region APAccount
    public class APAccount : Account
    {
        public APAccount(XmlNode node)
        {
            throw new OFXParseException("AP Account type not supported");
        }
        public override void ImportTransactions(OFXDocumentParser parser, OFXDocument ofx, XmlDocument doc)
        {
        }
    }
    #endregion

    #region ARAccount
    public class ARAccount : Account
    {
        public ARAccount(XmlNode node)
        {
            throw new OFXParseException("AR Account type not supported");
        }

        public override void ImportTransactions(OFXDocumentParser parser, OFXDocument ofx, XmlDocument doc)
        {
        }
    }
    #endregion
}
