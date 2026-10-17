public static class PotatoUnlock
{
    // 게임에서 조건 달성 시 호출 → 런처와 공유되는 저장소에 기록
    public static void Unlock(string gameName)
    {
        SharedUnlockStore.Unlock(gameName);
    }
}
