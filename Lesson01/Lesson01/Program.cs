using System;

namespace Lesson01
{
	class MainClass
	{
		public static void Main (string[] args)
		{
//			string s = "Hello world!";

			double n = 100;
			double d = n * Math.Log (n);
			Console.WriteLine (d);

			/// 1.冒泡排序 n ~ Math.Sqrt(n)
//			int[] ints = new int[]{10,8,2,5,6,9,1,3};
//
//			Console.WriteLine ("冒泡排序前数组：");
//			foreach (int num in ints) {
//				Console.Write (num + "，");
//			}
//			Console.WriteLine ();
//
//			//	冒泡排序
//			for (int i = 0; i < ints.Length - 1; i++) {
//				for (int j = 0; j < ints.Length - i - 1; j++) {
//					int tmp;
//					if (ints [j] > ints [j + 1]) {
//						tmp = ints [j];
//						ints [j] = ints [j + 1];
//						ints [j + 1] = tmp;
//					}
//				}
//			}
//
//			Console.WriteLine ("冒泡排序后数组：");
//			foreach (int num in ints) {
//				Console.Write (num + "，");
//			}
//			Console.WriteLine ();

		}
		/// <summary>
		/// Jssort the specified a.
		/// </summary>
		/// <param name="a">The alpha component.</param>
		void jssort(int[] a)
		{

		}
	}
}

///

//继承UIDragDropItem,实现UI可拖拽功能

public class BagItem : UIDragDropItem {

	private UISprite sprite;

	void Awake(){
		base.Awake ();
		sprite = this.GetComponent<UISprite> ();
	}


	protected override void OnDragDropRelease(GameObject surface){	//每次UI拖拽结束后都会调用这个函数，surface表示拖拽后鼠标下的物体
		base.OnDragDropRelease (surface);
		if (surface != null) {					//鼠标下物体不为空，先判断下鼠标下的物体时什么
			if (surface.tag == Tags.bag_item_grid) {	//拖放到空的格子里面

				if (surface == this.transform.parent.gameObject) {	//拖放到自己的格子里，位置归零

				} else {						//拖放到一个空的格子里
					BagItemGrid oldparent = this.transform.parent.GetComponent<BagItemGrid>();//取得原有格子的信息
					this.transform.parent = surface.transform;				//将其父亲更改为新的空格子
					ResetPosition ();
					BagItemGrid newparent = surface.GetComponent<BagItemGrid>();		//取得新的空格子
					newparent.SetId (oldparent.id, oldparent.num);				//将信息存放到新的格子里
					oldparent.CleanrInfo ();						//清除原来格子的信息
				}

			} else if (surface.tag == Tags.bag_item) {						//拖放到有物体个格子里
				BagItemGrid Grid1 = this.transform.parent.GetComponent<BagItemGrid>();		//取得现在格子的信息
				BagItemGrid Grid2 = surface.transform.parent.GetComponent<BagItemGrid>();	//取得拖放到格子的信息
				int id = Grid1.id;								//保存原有格子的信息
				int num = Grid1.num;
				Grid1.SetId (Grid2.id, Grid2.num);						//交换两个格子的信息
				Grid2.SetId (id, num);
			}
		} 

		ResetPosition ();
	}

	void ResetPosition(){
		transform.localPosition = Vector3.zero;
	}

	public void SetId(int id){
		ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById (id);
		sprite.spriteName = info.icon_name;
	}

	public void SetIconName(string icon_name){
		sprite.spriteName = icon_name;
	}

}