//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// This script can be used to stretch objects relative to the screen's width and height.
/// The most obvious use would be to create a full-screen background by attaching it to a sprite.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch : MonoBehaviour
{
	public enum Style
	{
		None,
		Horizontal,
		Vertical,
		Both,
		BasedOnHeight,
		FillKeepingRatio, // Fits the image so that it entirely fills the specified container keeping its ratio
		FitInternalKeepingRatio // Fits the image/item inside of the specified container keeping its ratio
	}

	/// <summary>
	/// Camera used to determine the anchor bounds. Set automatically if none was specified.
	/// </summary>

	public Camera uiCamera = null;

	/// <summary>
	/// Object used to determine the container's bounds. Overwrites the camera-based anchoring if the value was specified.
	/// </summary>

	public GameObject container = null;

	/// <summary>
	/// Stretching style.
	/// </summary>

	public Style style = Style.None;

	/// <summary>
	/// Whether the operation will occur only once and the script will then be disabled.
	/// </summary>

	public bool runOnlyOnce = false;

	/// <summary>
	/// Relative-to-target size.
	/// </summary>

	public Vector2 relativeSize = Vector2.one;

	/// <summary>
	/// The size that the item/image should start out initially.
	/// Used for FillKeepingRatio, and FitInternalKeepingRatio.
	/// Contributed by Dylan Ryan.
	/// </summary>

	public Vector2 initialSize = Vector2.one;

	// Deprecated legacy functionality
	[HideInInspector][SerializeField] UIWidget widgetContainer;

	Transform mTrans;
	UIWidget mWidget;
	UIRoot mRoot;
	Animation mAnim;
	Rect mRect;

	void OnEnable ()
	{
		mAnim = animation;
		mRect = new Rect();
		mTrans = transform;
		mWidget = GetComponent<UIWidget>();
	}

	void Start ()
	{
		if (container == null && widgetContainer != null)
		{
			container = widgetContainer.gameObject;
			widgetContainer = null;
#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif
		}

		if (uiCamera == null) uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
		mRoot = NGUITools.FindInParents<UIRoot>(gameObject);
		Update();
	}

	void Update ()
	{
		if (mAnim != null && mAnim.isPlaying) return;

		if (style != Style.None)
		{
			UIWidget wc = (container == null) ? null : container.GetComponent<UIWidget>();
			UIPanel pc = (container == null && wc == null) ? null : container.GetComponent<UIPanel>();
			float adjustment = 1f;

			if (wc != null)
			{
				Bounds b = wc.CalculateBounds(transform.parent);

				mRect.x = b.min.x;
				mRect.y = b.min.y;

				mRect.width = b.size.x;
				mRect.height = b.size.y;
			}
			else if (pc != null)
			{
				if (pc.clipping == UIDrawCall.Clipping.None)
				{
					// Panel has no clipping -- just use the screen's dimensions
					float ratio = (mRoot != null) ? (float)mRoot.activeHeight / Screen.height * 0.5f : 0.5f;
					mRect.xMin = -Screen.width * ratio;
					mRect.yMin = -Screen.height * ratio;
					mRect.xMax = -mRect.xMin;
					mRect.yMax = -mRect.yMin;
				}
				else
				{
					// Panel has clipping -- use it as the mRect
					Vector4 pos = pc.clipRange;
					mRect.x = pos.x - (pos.z * 0.5f);
					mRect.y = pos.y - (pos.w * 0.5f);
					mRect.width = pos.z;
					mRect.height = pos.w;
				}
			}
			else if (container != null)
			{
				Transform root = transform.parent;
				Bounds b = (root != null) ? NGUIMath.CalculateRelativeWidgetBounds(root, container.transform) :
					NGUIMath.CalculateRelativeWidgetBounds(container.transform);

				mRect.x = b.min.x;
				mRect.y = b.min.y;

				mRect.width = b.size.x;
				mRect.height = b.size.y;
			}
			else if (uiCamera != null)
			{
				mRect = uiCamera.pixelRect;
			}
			else return;

			float rectWidth = mRect.width;
			float rectHeight = mRect.height;

			if (adjustment != 1f && rectHeight > 1f)
			{
				float scale = mRoot.activeHeight / rectHeight;
				rectWidth *= scale;
				rectHeight *= scale;
			}

			Vector3 size = mWidget != null ? new Vector3(mWidget.width, mWidget.height) : mTrans.localScale;

			if (style == Style.BasedOnHeight)
			{
				size.x = relativeSize.x * rectHeight;
				size.y = relativeSize.y * rectHeight;
			}
			else if (style == Style.FillKeepingRatio)
			{
				// Contributed by Dylan Ryan
				float screenRatio = rectWidth / rectHeight;
				float imageRatio = initialSize.x / initialSize.y;

				if (imageRatio < screenRatio)
				{
					// Fit horizontally
					float scale = rectWidth / initialSize.x;
					size.x = rectWidth;
					size.y = initialSize.y * scale;
				}
				else
				{
					// Fit vertically
					float scale = rectHeight / initialSize.y;
					size.x = initialSize.x * scale;
					size.y = rectHeight;
				}
			}
			else if (style == Style.FitInternalKeepingRatio)
			{
				// Contributed by Dylan Ryan
				float screenRatio = rectWidth / rectHeight;
				float imageRatio = initialSize.x / initialSize.y;

				if (imageRatio > screenRatio)
				{
					// Fit horizontally
					float scale = rectWidth / initialSize.x;
					size.x = rectWidth;
					size.y = initialSize.y * scale;
				}
				else
				{
					// Fit vertically
					float scale = rectHeight / initialSize.y;
					size.x = initialSize.x * scale;
					size.y = rectHeight;
				}
			}
			else
			{
				if (style == Style.Both || style == Style.Horizontal) size.x = relativeSize.x * rectWidth;
				if (style == Style.Both || style == Style.Vertical) size.y = relativeSize.y * rectHeight;
			}

			UIWidget w = mTrans.GetComponent<UIWidget>();

			if (w != null)
			{
				w.width = Mathf.RoundToInt(size.x);
				w.height = Mathf.RoundToInt(size.y);
				size = Vector3.one;
			}
			
			if (mTrans.localScale != size)
				mTrans.localScale = size;

			if (runOnlyOnce && Application.isPlaying) Destroy(this);
		}
	}
}
