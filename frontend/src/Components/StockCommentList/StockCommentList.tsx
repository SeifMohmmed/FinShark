import StockCommentListItem from "../StockCommentListItem/StockCommentListItem";
import type { CommentGet } from "../../Models/Comment";

type Props = {
  comments: CommentGet[];
};

const StockCommentList = ({ comments }: Props) => {
  return (
    <>
      {comments
        ? comments.map((comment) => {
            return <StockCommentListItem comment={comment} />;
          })
        : ""}
    </>
  );
};

export default StockCommentList;
