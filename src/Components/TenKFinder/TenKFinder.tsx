import React, { useEffect, useState } from "react";
import { CompanyTenk } from "../../company";
import { getTenk } from "../../api";
import TenKFinderItem from "./TenkFinderItem/TenKFinderItem";
import Spinner from "../Spinners/Spinner";

type Props = {
  ticker: string;
};

const TenKFinder = ({ ticker }: Props) => {
  const [companyData, setCompanyData] = useState<CompanyTenk[]>();
  useEffect(() => {
    const getTenKData = async () => {
      const value = await getTenk(ticker);
      setCompanyData(value?.data);
    };
    getTenKData();
  }, [ticker]);
  return (
    <div className="inline-flex rounded-md shadow-sm m-4" role="group">
      {companyData ? (
        companyData?.slice(0, 5).map((tenK) => {
          return <TenKFinderItem tenK={tenK} />;
        })
      ) : (
        <Spinner />
      )}
    </div>
  );
};

export default TenKFinder;