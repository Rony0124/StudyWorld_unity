using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouch : MonoBehaviour
{
    [SerializeField] private bool isRotateX = true;
    [SerializeField] private bool isRotateY = true;
    [SerializeField] private bool isAdjust = true;
    [SerializeField] private float rotationSpeedX = 0.08f;
    [SerializeField] private float rotationSpeedY = 0.5f;
    [SerializeField] private float sizeAdjustmentSpeed = 1f;
    [SerializeField] private float posAdjustmentSpeed = 0.7f;
    [SerializeField] private float beginRotateThreshold;
    [SerializeField] private float beginAdjustThreshold;
    [SerializeField] private RectTransform ignoreArea;
    [SerializeField] private bool isUseTailing;

    public float AccumulatedXRotation { get; set; }

    private bool isRotateBegin;
    private bool isAdjustBegin;
    private bool isEndRotating;
    private bool isDragBegin;
    private bool isEndDragging;
    private Vector2 lastFingerSwipeScaledDelta;
    private float pinchDelta;
    private Touch touch;

    private readonly float MAX_ROTATION_X = 90;
    private readonly float MAX_CAMERA_SCALE = 2.7f;
    private readonly float MIN_CAMERA_SCALE = 1f;
    private readonly float SWIPE_THRESHOLD = 5;
    private readonly int OFFSET_AMOUNT = 40;

    private void Start() {

        //if (LeanTouch.Instance == null) {
        //    var leanTouchObject = new GameObject("LeanTouch");
        //    leanTouch = leanTouchObject.AddComponent<LeanTouch>();
        //}
    }

    //private void Update() {
    //    var fingers = LeanTouch.GetFingers(false);

    //    switch (fingers.Count) {
    //        case 0:
    //            NonFingerAction();
    //            break;
    //        case 1:
    //            SingleFingerAction(fingers);
    //            break;
    //        case 2:
    //            MultiFingerAction(fingers);
    //            break;
    //    }
    //}

    //private void OnDestroy() {
    //    if (leanTouch) {
    //        Destroy(leanTouch.gameObject);
    //    }
    //}

    //#region 0finger
    //private void NonFingerAction() {
    //    if (isRotateBegin) {
    //        RotateTween(lastFingerSwipeScaledDelta).Forget();
    //    }

    //    if (isDragBegin) {
    //        DragTween(lastFingerSwipeScaledDelta).Forget();
    //    }

    //    pinchDelta = 0;
    //    isRotateBegin = false;
    //    isAdjustBegin = false;
    //    isDragBegin = false;
    //}

    //private async UniTaskVoid RotateTween(Vector2 angle) {
    //    if (angle.magnitude < SWIPE_THRESHOLD) {
    //        return;
    //    }

    //    if (!isRotateX) {
    //        angle.y = 0;
    //    }

    //    if (!isRotateY) {
    //        angle.x = 0;
    //    }

    //    var xAnglePerFrame = angle.y / 50;
    //    var yAnglePerFrame = -angle.x / 25;
    //    var angleRatio = 1.5f;
    //    isEndRotating = true;

    //    while (isEndRotating && angleRatio > 0f) {
    //        RotateTarget(xAnglePerFrame * angleRatio, yAnglePerFrame * angleRatio);
    //        await UniTask.DelayFrame(1);
    //        angleRatio -= 0.1f;
    //    }

    //    isEndRotating = false;
    //}

    //private async UniTaskVoid DragTween(Vector2 distance) {
    //    if (distance.magnitude < SWIPE_THRESHOLD) {
    //        return;
    //    }

    //    var distanceRatio = 1.5f;
    //    isEndDragging = true;

    //    while (isEndDragging && distanceRatio > 0f) {
    //        DragTarget(distance / 50);
    //        await UniTask.DelayFrame(1);
    //        distanceRatio -= 0.15f;
    //    }

    //    isEndDragging = false;
    //}
    //#endregion
    //#region 1finger
    //private void SingleFingerAction(List<LeanFinger> fingers) {
    //    isEndRotating = false;

    //    if (!IsFingerAvailable(fingers)) {
    //        return;
    //    }

    //    isAdjustBegin = false;
    //    var finger = fingers.First();
    //    lastFingerSwipeScaledDelta = finger.GetSnapshotScaledDelta(0.05f);

    //    RotateTargetWithFinger(finger);
    //}

    //private void RotateTargetWithFinger(LeanFinger leanFinger) {
    //    var xAngle = isRotateX ? leanFinger.ScreenDelta.y * rotationSpeedX : 0f;
    //    var yAngle = isRotateY ? -leanFinger.ScreenDelta.x * rotationSpeedY : 0f;

    //    RotateTarget(xAngle / 2, yAngle);

    //    if (!isRotateBegin && leanFinger.ScreenDelta.magnitude > beginRotateThreshold) {
    //        isRotateBegin = true;
    //        onBeginRotate?.Invoke();
    //    }
    //}

    //public void RotateTarget(float xAngle, float yAngle) {
    //    if ((xAngle > 0 && AccumulatedXRotation > MAX_ROTATION_X) ||
    //        (xAngle < 0 && AccumulatedXRotation < -MAX_ROTATION_X)) {
    //        xAngle = 0;
    //    }

    //    transform.Rotate(0, yAngle, 0, Space.World);
    //    transform.Rotate(xAngle, 0, 0, Space.Self);

    //    AccumulatedXRotation += xAngle;
    //}
    //#endregion

    //#region 2fingers
    //private void MultiFingerAction(List<LeanFinger> fingers) {
    //    isEndRotating = false;
    //    isEndDragging = false;
    //    lastFingerSwipeScaledDelta = LeanGesture.GetScaledDelta();
    //    pinchDelta = 1 - LeanGesture.GetPinchRatio();
    //    float threshold = Math.Abs(pinchDelta);

    //    if (!IsFingerAvailable(fingers)) {
    //        return;
    //    }

    //    if (threshold <= 0.025f) {
    //        DragTarget(lastFingerSwipeScaledDelta);
    //    }
    //    else {
    //        AdjustTargetSize();
    //    }

    //    isRotateBegin = false;
    //}

    //private void DragTarget(Vector2 dist) {
    //    var modelPosition = transform.localPosition * Vector2.one;
    //    var posDelta = modelPosition + dist / OFFSET_AMOUNT * posAdjustmentSpeed;
    //    transform.localPosition = new Vector2(Mathf.Clamp(posDelta.x, -7f, 7f), Mathf.Clamp(posDelta.y, -4f, 4f));
    //    isDragBegin = true;
    //}

    //private void AdjustTargetSize() {
    //    if (!isAdjust) {
    //        return;
    //    }

    //    var pinchRatio = LeanGesture.GetPinchRatio();
    //    var currentScale = transform.localScale.magnitude / Vector3.one.magnitude;
    //    var newScale =
    //        Mathf.Clamp(currentScale + (1 - pinchRatio) * sizeAdjustmentSpeed, MIN_CAMERA_SCALE,
    //            MAX_CAMERA_SCALE);
    //    transform.localScale = newScale * Vector3.one;

    //    if (!isAdjustBegin && Mathf.Abs(1 - pinchRatio) > beginAdjustThreshold) {
    //        isAdjustBegin = true;
    //        onBeginAdjust?.Invoke();
    //    }
    //}
    //#endregion

    //private bool IsFingerAvailable(List<LeanFinger> fingers) {
    //    if (!ignoreArea) {
    //        return true;
    //    }

    //    return fingers.Any(finger =>
    //        !RectTransformUtility.RectangleContainsScreenPoint(ignoreArea, finger.StartScreenPosition));
    //}
}
